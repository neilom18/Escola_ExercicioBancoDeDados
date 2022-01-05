using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Escola_ExercicioBancoDeDados.Repository
{
    public class AlunoRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionStting => _configuration.GetConnectionString("AppAcademy");
        public AlunoRepository(IConfiguration configuration)
        {
               _configuration = configuration;
        }

        public void Insert(Aluno aluno)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using var cmd = new OracleCommand
                    (
                        @"INSERT INTO APPACADEMY.ALUNO
                    (NOME, IDADE, TURMA_ID, ID)
                    VALUES(:Nome, :Idade, :Turma_Id, :Id)", conn
                    );

                cmd.Parameters.Add(new OracleParameter("NOME", aluno.Nome));
                cmd.Parameters.Add(new OracleParameter("IDADE", aluno.Idade));
                cmd.Parameters.Add(new OracleParameter("TURMA_ID", aluno.Turma.Id.ToString()));
                cmd.Parameters.Add(new OracleParameter("ID", aluno.Id.ToString()));

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro ao tentar adicionar o aluno");
            }
        }

        public Aluno SelectById(Guid id)
        {
            using var conn = new OracleConnection(ConnectionStting);

            conn.Open();

            using var cmd = new OracleCommand
                (@"
                SELECT a.NOME AS aluno_nome,
                a.ID AS aluno_id,
                a.IDADE,
                c.ID AS curso_id,
                c.NOME AS curso_nome,
                t.ID AS turma_id
                FROM ALUNO a 
                LEFT JOIN TURMA t
                ON a.TURMA_ID = t.ID
                LEFT JOIN CURSO c 
                ON c.ID = t.CURSO_ID
                WHERE a.ID = :Id", conn);

            cmd.Parameters.Add("Id", id.ToString());

            using ( var reader = cmd.ExecuteReader())
            {
                Aluno aluno = null;
                while (reader.Read())
                {
                    if(Guid.TryParse(reader["turma_id"].ToString(), out var guid))
                    {
                        aluno = new Aluno(
                            nome: reader["aluno_nome"].ToString(),
                            idade: int.Parse(reader["idade"].ToString()),
                            id: Guid.Parse(reader["aluno_id"].ToString()),
                            turma: new Turma(
                                new Curso(
                                    nome: reader["curso_nome"].ToString(),
                                    id: Guid.Parse(reader["curso_id"].ToString())
                                    ),
                                id:guid
                                )
                            );
                    }
                    else
                    {
                        aluno = new Aluno(
                        nome: reader["aluno_nome"].ToString(),
                        idade: int.Parse(reader["idade"].ToString()),
                        id: Guid.Parse(reader["aluno_id"].ToString()));
                    }
                }
                return aluno;
            }
        }

        public List<Aluno> SelectByParam(AlunoQuery alunoQuery)
        {
            List<Aluno> alunos = new List<Aluno>();
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
                        SELECT al.NOME AS aluno_nome,
                        al.IDADE,
                        al.ID AS aluno_id,
                        al.TURMA_ID,
                        t.CURSO_ID,
                        c.NOME AS curso_nome
                        FROM ALUNO al
                        LEFT JOIN TURMA t
                        ON t.ID = al.TURMA_ID
                        LEFT JOIN CURSO c
                        ON t.CURSO_ID = c.ID
                        WHERE 1 = 1");
                    if (alunoQuery.Nome != null)
                    {
                        sb.Append(" AND al.NOME = :Nome");
                        parameterCollection.Add("Nome", alunoQuery.Nome);
                    }
                    if (alunoQuery.Idade != null)
                    {
                        sb.Append(" AND al.IDADE = :Idade");
                        parameterCollection.Add("Idade", alunoQuery.Idade);
                    }
                    if (alunoQuery.Turma_Id != null)
                    {
                        sb.Append(" AND al.TURMA_ID = :Turma_id");
                        parameterCollection.Add("Turma_id", alunoQuery.Turma_Id.Value.ToString());
                    }
                    if (alunoQuery.Curso_Id != null)
                    {
                        sb.Append(" AND al.TURMA_ID IN (SELECT ID FROM TURMA t WHERE t.CURSO_ID = :Curso_id)");
                        parameterCollection.Add("Curso_id", alunoQuery.Curso_Id.Value.ToString());
                    }
                    sb.Append(@") a
                        WHERE rownum < ((:pageNumber * :pageSize) + 1 )
                        )
                        WHERE r__ >= (((:pageNumber-1) * :pageSize) + 1)");

                    parameterCollection.Add("pageNumber", alunoQuery.Page);
                    parameterCollection.Add("pageSize", alunoQuery.Size);
                    using (var cmd = new OracleCommand(sb.ToString(), conn))
                    {
                        cmd.BindByName = true;
                        foreach (var parameter in parameterCollection)
                            cmd.Parameters.Add(parameter.Key, parameter.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Guid.TryParse(reader["turma_id"].ToString(), out var guid))
                                {
                                    alunos.Add(new Aluno(
                                        nome: reader["aluno_nome"].ToString(),
                                        idade: int.Parse(reader["idade"].ToString()),
                                        id: Guid.Parse(reader["aluno_id"].ToString()),
                                        turma: new Turma(
                                            new Curso(
                                                nome: reader["curso_nome"].ToString(),
                                                id: Guid.Parse(reader["curso_id"].ToString())
                                                ),
                                            id: guid
                                            )
                                        ));
                                }
                                else
                                {
                                    alunos.Add(new Aluno(
                                    nome: reader["aluno_nome"].ToString(),
                                    idade: int.Parse(reader["idade"].ToString()),
                                    id: Guid.Parse(reader["aluno_id"].ToString())
                                    ));
                                }
                            }
                        }
                    }
                }
                return alunos;
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro na busca por parametros do aluno");
            }
        }

        public List<Materia> GetMateriasFromAluno(Guid id)
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
                (SELECT am.MATERIA_ID 
                FROM ALUNO_MATERIA am 
                WHERE am.ALUNO_ID = :Id)", conn
                    ))
                {
                    cmd.Parameters.Add("Id", id.ToString());
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
                throw new Exception("Houve um erro ao buscar as materias do aluno");
            }
        }

        public int Update(Aluno aluno)
        {
            try
            {
                using var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                using (var cmd = new OracleCommand
                    (
                    @"UPDATE APPACADEMY.ALUNO
                SET NOME = :Nome, IDADE = :Idade, TURMA_ID = :Turma_id
                WHERE ID = :Id", conn
                    ))
                {
                    cmd.Parameters.Add("Nome", aluno.Nome);
                    cmd.Parameters.Add("Idade", aluno.Idade);
                    cmd.Parameters.Add("Turma_id", aluno.Turma.Id.ToString());
                    cmd.Parameters.Add("Id", aluno.Id.ToString());

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro na atualização do aluno");
            }
        }
        public int Delete(Guid id)
        {
            try
            {
                var conn = new OracleConnection(ConnectionStting);

                conn.Open();

                var cmd1 = new OracleCommand(@"DELETE FROM ALUNO_MATERIA WHERE ALUNO_ID = :Id", conn);
                cmd1.Parameters.Add("Id", id.ToString());
                cmd1.ExecuteNonQuery();
                var cmd2 = new OracleCommand(@"DELETE FROM ALUNO WHERE ID = :Id", conn);
                cmd2.Parameters.Add("Id", id.ToString());
                return cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro no delete");
            }
        }
    }
}
