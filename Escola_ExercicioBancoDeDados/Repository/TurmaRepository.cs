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

        
        
        /*public Turma SelectById(Guid id)
        {
            using var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            var cmd = new OracleCommand
                (
                    @"SELECT * FROM MATERIA WHERE ID = :Id", conn
                );

            cmd.Parameters.Add("Id", id);

            using (var reader = cmd.ExecuteReader())
            {
                

                return turma;
            }
        }*/
    }
}
