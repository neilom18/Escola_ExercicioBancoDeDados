using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class ProfessorRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");
        public ProfessorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Insert(Professor professor)
        {
            try
            {
                var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                        @"INSERT INTO APPACADEMY.PROFESSOR
                    (ID, NOME, IDADE)
                    VALUES(:Id, :Nome, :Idade)", conn
                    );

                cmd.BindByName = true;

                cmd.Parameters.Add("Id", professor.Id.ToString());
                cmd.Parameters.Add("Nome", professor.Nome);
                cmd.Parameters.Add("Idade", professor.Idade);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao tentar adicionar o professor");
            }
        }

        public Professor SelectById(Guid id)
        {
            var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            var cmd = new OracleCommand
                (
                    @"SELECT * FROM PROFESSOR WHERE ID = :Id", conn
                );

            cmd.BindByName = true;
            cmd.Parameters.Add("Id", id.ToString());

            using (var reader = cmd.ExecuteReader())
            {
                if(!reader.HasRows) return null;
                reader.Read();

                return new Professor(
                    nome: reader["nome"].ToString(),
                    idade: int.Parse(reader["idade"].ToString()),
                    id: Guid.Parse(reader["id"].ToString()));
            }
        }
    }
}
