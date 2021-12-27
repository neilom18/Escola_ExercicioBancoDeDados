using Escola_ExercicioBancoDeDados.Endity;
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
                        VALUES(:Materia_id, :Curso_id, :Id);", conn
                            );

                        cmd.Parameters.Add("Materia_id", materia.Id);
                        cmd.Parameters.Add("Curso_id", curso.Id);
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
    }
}
