﻿using Dapper;
using Dominio.IRepository.Dapper;
using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperContext.Repository
{
    public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepositoryDapper
    {
        public AlunoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override Aluno Get(Guid id)
        {
            return _connection.QuerySingle<Aluno>(@"
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
                WHERE a.ID = :Id", new { id });
        }

        public IEnumerable<Aluno> GetAllByParameters(AlunoQuery query)
        {
            try
            {
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
                if (query.Nome != null)
                {
                    sb.Append(" AND al.NOME = :aluno_nome");
                    parameterCollection.Add("aluno_nome", query.Nome);
                }
                if (query.Idade != null)
                {
                    sb.Append(" AND al.IDADE = :IDADE");
                    parameterCollection.Add("IDADE", query.Idade);
                }
                if (query.Turma_Id != null)
                {
                    sb.Append(" AND al.TURMA_ID = :Turma_id");
                    parameterCollection.Add("Turma_id", query.Turma_Id.Value.ToString());
                }
                if (query.Curso_Id != null)
                {
                    sb.Append(" AND al.TURMA_ID IN (SELECT ID FROM TURMA t WHERE t.CURSO_ID = :Curso_id)");
                    parameterCollection.Add("Curso_id", query.Curso_Id.Value.ToString());
                }
                sb.Append(@") a
                        WHERE rownum < ((:pageNumber * :pageSize) + 1 )
                        )
                        WHERE r__ >= (((:pageNumber-1) * :pageSize) + 1)");

                return _connection.Query<Aluno>(sb.ToString(), new { parameterCollection });
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro na busca dos alunos");
            }
        }
    }
}