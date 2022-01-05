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

        public int Update(Professor professor)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using (var cmd = new OracleCommand
                    (
                    @"UPDATE APPACADEMY.PROFESSOR
                SET NOME = :Nome, IDADE = :Idade
                WHERE ID = :Id", conn
                    ))
                {
                    cmd.Parameters.Add("Nome", professor.Nome);
                    cmd.Parameters.Add("Idade", professor.Idade);
                    cmd.Parameters.Add("Id", professor.Id.ToString());

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao atualizar o professor");
            }
        }

        public int Delete(Guid id)
        {
            try
            {
                int n;
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                OracleTransaction transaction = conn.BeginTransaction();

                using (var cmd = new OracleCommand
                    (
                    @"DELETE FROM MATERIA_CURSO WHERE MATERIA_ID = :Id", conn
                    ))
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("Id", id.ToString());
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new OracleCommand
                    (
                    @"DELETE FROM ALUNO_MATERIA WHERE MATERIA_ID = :Id", conn
                    ))
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("Id", id.ToString());
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new OracleCommand
                    (
                    @"DELETE FROM MATERIA WHERE PROFESSOR_ID = :Id"
                    ))
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("Id", id.ToString());
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new OracleCommand
                    (
                    @"DELETE FROM PROFESSOR WHERE ID = :Id", conn
                    ))
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("Id", id.ToString());
                    n = cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                return n;
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro no Delete da matéria");
            }
        }
    }
}
