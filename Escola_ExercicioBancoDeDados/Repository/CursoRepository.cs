using Escola_ExercicioBancoDeDados.DTO.QueryParameters;
using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

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
                curso.SetMaterias(materias);
                return curso;
            }
            catch 
            {
                throw new Exception("Ocoreu um erro na busca do curso");
            }
        }

        public IEnumerable<Curso> SelectByParam(CursoQuery cursoQuery)
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                using (var conn = new OracleConnection(ConnectionStting))
                {

                    conn.Open();

                    var parameterCollection = new Dictionary<string, object>();
                    StringBuilder sb = new StringBuilder(
                        @"SELECT * FROM
                        (
	                    SELECT a.*, rownum r__
	                    FROM
	                    (
                        SELECT 
                        c.ID AS curso_id,
                        c.NOME AS curso_nome
                        FROM CURSO c 
                        WHERE 1 = 1");
                    if (cursoQuery.Nome != null)
                    {
                        sb.Append(" AND c.NOME = :Curso_nome");
                        parameterCollection.Add("Curso_nome", cursoQuery.Nome);
                    }
                    if (cursoQuery.Professor_Nome != null)
                    {
                        sb.Append(@"AND c.ID IN (
                            SELECT mc.CURSO_ID 
                            FROM MATERIA_CURSO mc
                            WHERE mc.MATERIA_ID IN 
                            (SELECT m.ID FROM MATERIA m
                            WHERE m.PROFESSOR_ID
                            IN
                            (SELECT p.ID FROM PROFESSOR p
                            WHERE p.NOME = :Professor_nome)))");
                        parameterCollection.Add("Professor_nome", cursoQuery.Professor_Nome);
                    }

                    sb.Append(@") a
                        WHERE rownum < ((: pageNumber * :pageSize) + 1 )
                        )
                        WHERE r__ >= (((: pageNumber - 1) * :pageSize) +1)");

                    parameterCollection.Add("pageNumber", cursoQuery.Page);
                    parameterCollection.Add("pageSize", cursoQuery.Size);
                    using (var cmd = new OracleCommand(sb.ToString(), conn))
                    {
                        cmd.BindByName = true;
                        foreach (var parameter in parameterCollection)
                            cmd.Parameters.Add(parameter.Key, parameter.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cursos.Add(new Curso(
                                    nome: reader["curso_nome"].ToString(),
                                    id: Guid.Parse(reader["curso_id"].ToString())
                                    ));
                            }
                        }
                    }
                }
                return cursos;
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao buscar os cursos");
            }
        }

        public List<Materia> GetMateriasFromCursos(Guid id)
        {
            List<Materia> materias = new List<Materia>();
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using (var cmd = new OracleCommand
                    (
                    @"SELECT m.ID,
                m.NOME AS materia_nome,
                m.DESCRICAO,
                m.PROFESSOR_ID,
                p.NOME AS professor_nome,
                p.IDADE
                FROM MATERIA m
                LEFT JOIN PROFESSOR p
                ON p.ID = m.PROFESSOR_ID
                WHERE m.ID 
                IN 
                (SELECT mc.MATERIA_ID 
                FROM MATERIA_CURSO mc
                WHERE mc.CURSO_ID = :Curso_id)", conn
                    ))
                {
                    cmd.Parameters.Add("Curso_id", id.ToString());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materias.Add(new Materia
                                (nome: reader["materia_nome"].ToString(),
                                descricao: reader["descricao"].ToString(),
                                id: Guid.Parse(reader["id"].ToString()),
                                professor: new Professor(
                                    nome: reader["professor_nome"].ToString(),
                                    idade: int.Parse(reader["idade"].ToString()),
                                    id: Guid.Parse(reader["professor_id"].ToString()))
                                ));
                        }
                    }
                }
                return materias;
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao buscar as materias do curso");
            }
        }

        public void Update(Curso curso, OracleTransaction transaction)
        {
            try
            {
                using var cmd = new OracleCommand(@"UPDATE APPACADEMY.CURSO
                                                 SET NOME = :NOME
                                                 WHERE ID = :ID", transaction.Connection);
                cmd.Transaction = transaction;
                cmd.Parameters.Add("NOME", curso.Nome);
                cmd.Parameters.Add("ID", curso.Id.ToString());

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
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
