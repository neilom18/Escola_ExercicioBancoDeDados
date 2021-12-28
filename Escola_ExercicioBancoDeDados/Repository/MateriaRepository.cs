using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class MateriaRepository
    {

        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");

        public MateriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Materia Insert(Materia materia)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                       @"INSERT INTO APPACADEMY.MATERIA
                    (NOME, DESCRICAO, ID)
                    VALUES(:Nome, :Descricao, :Id)", conn
                    );

                cmd.Parameters.Add("Nome", materia.Nome);
                cmd.Parameters.Add("Descricao", materia.Descricao);
                cmd.Parameters.Add("Id", materia.Id.ToString());

                cmd.ExecuteNonQuery();
                return materia;
            }
            catch
            {
                throw new Exception("Houve um erro ao adicionar a matéria");
            }
        }

        public Materia SelectById(Guid id)
        {
            using var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            var cmd = new OracleCommand
                (
                    @"SELECT * FROM MATERIA WHERE ID = :Id", conn
                );

            cmd.Parameters.Add("Id", id.ToString());

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                var materia = new Materia
                    (
                    nome: reader["nome"].ToString(),
                    descricao: reader["descricao"].ToString(),
                    id: Guid.Parse(reader["id"].ToString())
                    );
                return materia;
            }
        }
    }
}
