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
                        while (reader.Read())
                        {
                            curso = new Curso(
                               nome: reader["Nome"].ToString(),
                               id: Guid.Parse(reader["Id"].ToString())
                               );
                        }
                    }
                }
                using (var cmd = new OracleCommand
                    (
                    @"SELECT m.ID AS materia_id, m.Nome, m.DESCRICAO, m.PROFESSOR_ID, p.Nome AS professor_nome, p.IDADE
                    FROM MATERIA m
                    LEFT JOIN PROFESSOR p
                    ON m.PROFESSOR_ID = p.ID
                    WHERE m.ID IN 
                    (SELECT materia_id FROM MATERIA_CURSO mc 
                    WHERE CURSO_ID = :Id)", conn
                    ))
                {
                    cmd.Parameters.Add("Id", curso.Id.ToString());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var materia = new Materia(nome:reader["nome"].ToString(),
                                descricao: reader["descricao"].ToString(),
                                id:Guid.Parse(reader["materia_id"].ToString()),
                                professor: new Professor(id: Guid.Parse(reader["professor_id"].ToString()),
                                                         nome: reader["professor_nome"].ToString(),
                                                         idade: int.Parse(reader["idade"].ToString())));

                            materias.Add(materia);
                        }
                    }
                }
                /*foreach(var materia in materias)
                {
                    using (var cmd = new OracleCommand
                        (
                        @"SELECT NOME, IDADE, ID 
                          FROM PROFESSOR WHERE ID = :Id", conn
                        )) 
                    {
                        cmd.Parameters.Add("Id", materia.Professor.Id.ToString());

                        using(var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var professor = new Professor(nome: reader["nome"].ToString(),
                                    idade: int.Parse(reader["idade"].ToString()),
                                    id: Guid.Parse(reader["id"].ToString()));

                                materia.SetProfessor(professor);
                            }
                        }
                    }*/
                curso.AddMaterias(materias);
                return curso;
            }
            catch 
            {
                throw new Exception("Ocoreu um erro na busca do curso");
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
                    @"DELETE FROM TURMA WHERE CURSO_ID = :Id", conn))
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("Id", id.ToString());
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new OracleCommand
                    (
                    @"DELETE FROM CURSO WHERE ID = :Id", conn
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
		        throw new Exception("Houve um erro ao tentar deletar um curso");
	        }
        }
    }
}
