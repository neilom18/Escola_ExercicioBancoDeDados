using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class CursoRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");

        public CursoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Curso Insert(Curso curso)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                       @"INSERT INTO APPACADEMY.CURSO
                    (NOME, ID)
                    VALUES(:Nome, :Id)", conn
                    );

                cmd.Parameters.Add("Nome", curso.Nome);
                cmd.Parameters.Add("Id", curso.Id.ToString());

                var a = cmd.ExecuteNonQuery();
                return curso;
            }
            catch
            {
                throw new Exception("Houve um erro ao tentar adicionar o curso");
            }
        }
        public Curso Insert(Curso curso, OracleCommand command)
        {
            try
            {
                command.Parameters.Add("Nome", curso.Nome);
                command.Parameters.Add("Id", curso.Id.ToString());

                var a = command.ExecuteNonQuery();
                return curso;
            }
            catch
            {
                throw new Exception("Houve um erro ao tentar adicionar o curso");
            }
        }

        public Curso SelectById(Guid id)
        {
            try 
            {
                var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                Curso curso = null;
                List<Materia> materias = new List<Materia>();
                using (var cmd = new OracleCommand 
                    (
                    @"SELECT * FROM CURSO WHERE ID = :Id", conn
                    ))
                {
                    cmd.Parameters.Add("Id", id.ToString());

                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        curso = new Curso(
                           nome: reader["Nome"].ToString(),
                           id: Guid.Parse(reader["Id"].ToString())
                           );
                    }
                }
                using (var cmd = new OracleCommand
                    (
                    @"SELECT * FROM MATERIA WHERE ID IN 
                    (SELECT materia_id FROM MATERIA_CURSO
                    WHERE CURSO_ID = :Id)", conn
                    ))
                {
                    cmd.Parameters.Add("Id", curso.Id.ToString());
                    using (var reader2 = cmd.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            var materia = new Materia(nome:reader2["nome"].ToString(),
                                descricao: reader2["descricao"].ToString(),
                                id:Guid.Parse(reader2["id"].ToString()));

                            materias.Add(materia);
                        }
                        curso.AddMaterias(materias);
                    }
                }
                return curso;
            }
            catch 
            {
                throw new Exception("Ocoreu um erro na busca do curso");
            }
        }
    }
}
