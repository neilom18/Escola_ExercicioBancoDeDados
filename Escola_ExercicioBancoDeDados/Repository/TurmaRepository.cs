using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class TurmaRepository
    {

        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");
        public TurmaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Turma Insert(Turma turma)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                var cmd = new OracleCommand
                    (
                        @"INSERT INTO APPACADEMY.TURMA
                    (CURSO_ID, ID)
                    VALUES(:Curso_id, :Id)", conn
                    );

                cmd.Parameters.Add("Curso_id", turma.Curso.Id.ToString());
                cmd.Parameters.Add("Id", turma.Id.ToString());


                cmd.ExecuteNonQuery();
                return turma;
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao tentar adicionar a turma");
            }
        }

        public Guid GetCursoId(Guid id)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using (var cmd = new OracleCommand
                    (
                        @"SELECT CURSO_ID FROM TURMA WHERE ID = :Id", conn
                    ))
                {

                    cmd.Parameters.Add("Id", id.ToString());
                    Guid Curso_Id = Guid.Empty;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Curso_Id = Guid.Parse(reader["curso_id"].ToString());
                        }
                        return Curso_Id;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar o id do curso");
            }
        }

        public List<Aluno> GetAlunos(Guid id)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                        @"SELECT * FROM ALUNO a WHERE ID IN (SELECT ID FROM TURMA WHERE :Id = a.TURMA_ID)", conn
                    );

                cmd.Parameters.Add("Id", id.ToString());
                var alunos = new List<Aluno>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno(
                            nome: reader["nome"].ToString(),
                            idade: int.Parse(reader["idade"].ToString()),
                            id: Guid.Parse(reader["id"].ToString()));

                        alunos.Add(aluno);
                    }
                }
                return alunos;
            }
            catch (Exception)
            {

                throw new Exception("Ocorreu um erro ao buscar os alunos de uma turma");
            }
        }
    }
}
