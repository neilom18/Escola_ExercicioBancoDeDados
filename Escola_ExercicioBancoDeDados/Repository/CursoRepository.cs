using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class CursoRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");

        public CursoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Curso Insert(Curso curso)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                       @"INSERT INTO APPACADEMY.MATERIA
                    (NOME, DESCRICAO, ID)
                    VALUES(:Nome, :Id);", conn
                    );

                cmd.Parameters.Add("Nome", curso.Nome);
                cmd.Parameters.Add("Id", curso.Id);

                cmd.ExecuteNonQuery();
                return curso;
            }
            catch
            {
                throw new Exception("Houve um erro ao tentar adicionar o curso");
            }
        }
    }
}
