using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class AlunoRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");
        public AlunoRepository(IConfiguration configuration)
        {
               _configuration = configuration;
        }

        public void Insert(Aluno aluno)
        {
            using var conn = new OracleConnection(ConnectionStting);
            
            conn.Open();

            using var cmd = new OracleCommand
                (
                    @"INSERT INTO APPACADEMY.ALUNO
                    (NOME, IDADE, TURMA_ID, ID)
                    VALUES(:Nome, :Idade, :Turma_Id, :Id);", conn
                );

            cmd.Parameters.Add(new OracleParameter("NOME", aluno.Nome));
            cmd.Parameters.Add(new OracleParameter("IDADE", aluno.Idade));
            cmd.Parameters.Add(new OracleParameter("TURMA_ID", aluno.Turma_id));
            cmd.Parameters.Add(new OracleParameter("ID", aluno.Id.ToString()));

            cmd.ExecuteNonQuery();
        }

        public void GetById(Guid id)
        {
            using var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            using var cmd = new OracleCommand("select * from aluno", conn);
            using ( var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                   
                }
            }
        }

    }
}
