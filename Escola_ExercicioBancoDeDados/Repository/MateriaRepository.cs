using Dominio.Endity;
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
                    (NOME, DESCRICAO, ID, PROFESSOR_ID)
                    VALUES(:Nome, :Descricao, :Id, :Professor_id)", conn
                    );
                cmd.BindByName = true;
                cmd.Parameters.Add("Nome", materia.Nome);
                cmd.Parameters.Add("Descricao", materia.Descricao);
                cmd.Parameters.Add("Id", materia.Id.ToString());
                cmd.Parameters.Add("Professor_id", materia.Professor.Id.ToString());

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
                    @"SELECT m.ID ,m.DESCRICAO , m.NOME AS materia_nome, p.NOME AS professor_nome, m.PROFESSOR_ID, p.IDADE
                    FROM MATERIA m
                    LEFT JOIN PROFESSOR p
                    ON m.PROFESSOR_ID = p.ID
                    WHERE m.ID = :Id", conn
                );

            cmd.Parameters.Add("Id", id.ToString());

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                var materia = new Materia
                    (
                    nome: reader["materia_nome"].ToString(),
                    descricao: reader["descricao"].ToString(),
                    id: Guid.Parse(reader["id"].ToString()),
                    professor: new Professor(
                        nome: reader["professor_nome"].ToString(),
                        idade: int.Parse(reader["idade"].ToString()),
                        id: Guid.Parse(reader["professor_id"].ToString()))
                    );
                return materia;
            }
        }

        public int Update(Materia materia)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using (var cmd = new OracleCommand
                    (
                    @"UPDATE APPACADEMY.MATERIA
                SET NOME = :Nome, DESCRICAO = :Descricao, PROFESSOR_ID = :Professor_id
                WHERE ID = :Id", conn
                    ))
                {
                    cmd.Parameters.Add("Nome", materia.Nome);
                    cmd.Parameters.Add("Descricao", materia.Descricao);
                    cmd.Parameters.Add("Professor_id", materia.Professor.Id.ToString());
                    cmd.Parameters.Add("Id", materia.Id.ToString());

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro na atualização da matéria");
            }
        }

        public int Delete(Guid id)
        {
            try
            {
                int n = 0;
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
                    @"DELETE FROM MATERIA WHERE ID = :Id", conn
                    ))
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("Id", id.ToString());
                    n= cmd.ExecuteNonQuery();
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
