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
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                        @"INSERT INTO APPACADEMY.ALUNO
                    (NOME, IDADE, TURMA_ID, ID)
                    VALUES(:Nome, :Idade, :Turma_Id, :Id)", conn
                    );

                cmd.Parameters.Add(new OracleParameter("NOME", aluno.Nome));
                cmd.Parameters.Add(new OracleParameter("IDADE", aluno.Idade));
                cmd.Parameters.Add(new OracleParameter("TURMA_ID", aluno.Turma.Id.ToString()));
                cmd.Parameters.Add(new OracleParameter("ID", aluno.Id.ToString()));

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao tentar adicionar o aluno");
            }
        }

        public Aluno SelectById(Guid id)
        {
            using var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            using var cmd = new OracleCommand
                (@"
                SELECT a.NOME AS aluno_nome,
                a.ID AS aluno_id,
                a.IDADE,
                c.ID AS curso_id,
                c.NOME AS curso_nome,
                t.ID AS turma_id
                FROM ALUNO a 
                LEFT JOIN TURMA t
                ON a.TURMA_ID = t.ID
                LEFT JOIN CURSO c 
                ON c.ID = t.CURSO_ID
                WHERE a.ID = :Id", conn);

            cmd.Parameters.Add("Id", id.ToString());

            using ( var reader = cmd.ExecuteReader())
            {
                Aluno aluno = null;
                while (reader.Read())
                {
                    aluno = new Aluno(
                        nome: reader["aluno_nome"].ToString(),
                        idade: int.Parse(reader["idade"].ToString()),
                        id: Guid.Parse(reader["aluno_id"].ToString()),
                        turma: new Turma(
                            new Curso(
                                nome: reader["curso_nome"].ToString(),
                                id: Guid.Parse(reader["curso_id"].ToString())
                                ),
                            id: Guid.Parse(reader["turma_id"].ToString())
                            )
                        );
                }
                return aluno;
            }
        }

        public int Delete(Guid id)
        {
            try
            {
                var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                var cmd1 = new OracleCommand(@"DELETE FROM ALUNO_MATERIA WHERE ALUNO_ID = :Id", conn);
                cmd1.Parameters.Add("Id", id.ToString());
                cmd1.ExecuteNonQuery();
                var cmd2 = new OracleCommand(@"DELETE FROM ALUNO WHERE ID = :Id", conn);
                cmd2.Parameters.Add("Id", id.ToString());
                return cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro no delete");
            }
        }
    }
}
