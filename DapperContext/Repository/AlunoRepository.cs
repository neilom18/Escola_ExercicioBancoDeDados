using Dapper;
using Dominio.Endity;
using Dominio.IRepository.Dapper;
using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
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
                WHERE a.ID = :Id", new { Id = id });
        }

        public IEnumerable<Aluno> GetAllByParameters(AlunoQuery query)
        {
            try
            {
                var parameters = new DynamicParameters();
                StringBuilder sb = new StringBuilder(
                        @"
                        SELECT al.NOME,
                        al.IDADE,
                        al.ID
                        FROM ALUNO al
                        LEFT JOIN TURMA t
                        ON t.ID = al.TURMA_ID
                        LEFT JOIN CURSO c
                        ON t.CURSO_ID = c.ID
                        WHERE 1 = 1");
                if (query.Nome != null)
                {
                    sb.Append(" AND al.NOME = :aluno_nome");
                    parameters.Add("aluno_nome", query.Nome);
                }
                if (query.Idade != null)
                {
                    sb.Append(" AND al.IDADE = :IDADE");
                    parameters.Add("IDADE", query.Idade);
                }
                if (query.Turma_Id != null)
                {
                    sb.Append(" AND al.TURMA_ID = :Turma_id");
                    parameters.Add("Turma_id", query.Turma_Id.Value.ToString());
                }
                if (query.Curso_Id != null)
                {
                    sb.Append(" AND al.TURMA_ID IN (SELECT ID FROM TURMA t WHERE t.CURSO_ID = :Curso_id)");
                    parameters.Add("Curso_id", query.Curso_Id.Value.ToString());
                }
                

                var r = _connection.Query<Aluno>(sb.ToString(), parameters );
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Houve um erro na busca dos alunos");
            }
        }
    }
}
