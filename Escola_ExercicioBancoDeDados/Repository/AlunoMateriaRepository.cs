using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class AlunoMateriaRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");
        public AlunoMateriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Insert(Aluno aluno)
        {
            try
            {
                var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                foreach (var materia in aluno.Materias)
                {
                    using (var cmd = new OracleCommand
                        (
                        @"INSERT INTO APPACADEMY.ALUNO_MATERIA
                    (ID, MATERIA_ID, ALUNO_ID)
                    VALUES(:Id,:Materia_id, :Aluno_id)", conn
                        ))
                    {
                        cmd.BindByName = true;

                        cmd.Parameters.Add("Id", Guid.NewGuid().ToString());
                        cmd.Parameters.Add("Materia_id", materia.Id.ToString());
                        cmd.Parameters.Add("Aluno_id", aluno.Id.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao tentar adicionar a matéria ao aluno");
            }
        }
    }
}
