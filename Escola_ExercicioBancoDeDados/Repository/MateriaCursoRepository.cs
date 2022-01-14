using Dominio.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class MateriaCursoRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");

        public MateriaCursoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Insert(Curso curso)
        {

            try
            {
                using (var conn = new OracleConnection(ConnectionStting))
                {
                    conn.Open();
                    foreach (var materia in curso.Materias)
                    {

                        using var cmd = new OracleCommand
                            (
                               @"INSERT INTO APPACADEMY.MATERIA_CURSO
                        (MATERIA_ID, CURSO_ID, ID)
                        VALUES(:Materia_id, :Curso_id, :Id)", conn
                            );

                        cmd.Parameters.Add("Materia_id", materia.Id.ToString());
                        cmd.Parameters.Add("Curso_id", curso.Id.ToString());
                        cmd.Parameters.Add("Id", Guid.NewGuid().ToString());

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw new Exception("Houve um erro ao adicionar a matéria ao curso");
            }
        }

        public void Insert(Curso curso, OracleCommand command)
        {

            try
            {
                foreach (var materia in curso.Materias)
                {
                    var id = Guid.NewGuid().ToString();
                    command.Parameters.Add("Id", id);
                    command.Parameters.Add("Materia_id", materia.Id.ToString());
                    command.Parameters.Add("Curso_id", curso.Id.ToString());

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
            catch
            {
                throw new Exception("Houve um erro ao adicionar a matéria ao curso");
            }
        }

        public Materia SelectById(Guid curso_id, Guid materia_id)
        {
            try 
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using (var cmd = new OracleCommand
                    (
                    @"SELECT m.ID AS materia_id,
                    m.NOME AS materia_nome,
                    m.DESCRICAO AS descricao,
                    p.ID AS professor_id,
                    p.NOME AS professor_nome,
                    p.IDADE AS idade
                    FROM MATERIA m
                    LEFT JOIN MATERIA_CURSO mc 
                    ON mc.MATERIA_ID = m.ID
                    LEFT JOIN PROFESSOR p 
                    ON p.ID = m.PROFESSOR_ID
                    WHERE mc.MATERIA_ID = :Materia_id AND mc.CURSO_ID = :Curso_id", conn
                    ))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("materia_id", materia_id.ToString());
                    cmd.Parameters.Add("Curso_id", curso_id.ToString());

                    Materia materia = null;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materia = new Materia
                                (
                                nome: reader["materia_nome"].ToString(),
                                descricao: reader["descricao"].ToString(),
                                id: Guid.Parse(reader["materia_id"].ToString()),
                                professor: new Professor(
                                    id: Guid.Parse(reader["professor_id"].ToString()),
                                    nome: reader["professor_nome"].ToString(),
                                    idade: int.Parse(reader["idade"].ToString())
                                    )
                                );
                        }
                        return materia;
                    }
                }
            }
            catch 
            {
                throw new Exception("Houve um erro ao buscar a materia de um curso");
            }
        }

        public void Delete(Curso curso, OracleTransaction transaction)
        {
            using var cmd = new OracleCommand(@"DELETE FROM MATERIA_CURSO WHERE CURSO_ID = :CURSO_ID", transaction.Connection);

            cmd.Transaction = transaction;

            cmd.Parameters.Add("CURSO_ID", curso.Id.ToString());

            cmd.ExecuteNonQuery();
        }
    }
}
