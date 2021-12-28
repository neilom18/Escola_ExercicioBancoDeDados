using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

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
            using var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            using (var cmd = new OracleCommand
                (
                    @"SELECT * FROM MATERIA WHERE ID = :Id", conn
                ))
            {

                cmd.Parameters.Add("Id", id.ToString());

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    Guid Curso_Id = Guid.Parse(reader["curso_id"].ToString());
                    return Curso_Id;
                }
            }
        }
    }
}
