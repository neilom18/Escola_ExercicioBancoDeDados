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
                       @"INSERT INTO APPACADEMY.CURSO
                    (NOME, ID)
                    VALUES(:Nome, :Id)", conn
                    );

                cmd.Parameters.Add("Nome", curso.Nome);
                cmd.Parameters.Add("Id", curso.Id.ToString());

                var a = cmd.ExecuteNonQuery();
                return curso;
            }
            catch
            {
                throw new Exception("Houve um erro ao tentar adicionar o curso");
            }
        }
        public Curso Insert(Curso curso, OracleCommand command)
        {
            try
            {
                command.Parameters.Add("Nome", curso.Nome);
                command.Parameters.Add("Id", curso.Id.ToString());

                var a = command.ExecuteNonQuery();
                return curso;
            }
            catch
            {
                throw new Exception("Houve um erro ao tentar adicionar o curso");
            }
        }
    }
}
