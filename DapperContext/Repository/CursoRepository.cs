using Dapper;
using Dominio.Endity;
using Dominio.IRepository.Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperContext.Repository
{
    public class CursoRepository : BaseRepository<Curso>, ICursoRepositoryDapper
    {
        public CursoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override Curso Get(Guid id)
        {
            return _connection.QuerySingle<Curso>(@"SELECT * FROM CURSO
                                                  WHERE ID = :ID", new { ID = id });
        }

        public IEnumerable<Materia> GetMateriasFromCursos(Guid id)
        {
            return _connection.Query<Materia>(@"SELECT m.ID,
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
                WHERE mc.CURSO_ID = :CURSO_ID", new { CURSO_ID = id });
        }
    }
}
