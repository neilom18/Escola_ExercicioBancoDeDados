using Dapper;
using Dominio.IRepository.Dapper;
using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperContext.Repository
{
    public class TurmaRepository : BaseRepository<Turma>, ITurmaRepositoryDapper
    {
        public TurmaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override Turma Get(Guid id)
        {
            return _connection.QuerySingle<Turma>(@"SELECT * FROM TURMA t 
                                    LEFT JOIN CURSO c
                                    ON c.ID = t.CURSO_ID
                                    WHERE t.ID = :ID", new { ID = id });
        }

        public IEnumerable<Aluno> GetAlunos(Guid id)
        {
            return _connection.Query<Aluno>(@"SELECT * FROM ALUNO a
                                            WHERE TURMA_ID IN 
                                            (SELECT ID FROM TURMA
                                            WHERE ID = :Id)", new { ID = id });
        }
    }
}
