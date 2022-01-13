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
    public class MateriaRepository : BaseRepository<Materia>, IMateriaRepositoryDapper
    {
        public MateriaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override Materia Get(Guid id)
        {
           return _connection.QuerySingle<Materia>(@"SELECT m.ID,
                   m.DESCRICAO,
                   m.NOME AS materia_nome,
                   p.NOME AS professor_nome,
                   m.PROFESSOR_ID,
                   p.IDADE
                   FROM MATERIA m
                   LEFT JOIN PROFESSOR p
                   ON m.PROFESSOR_ID = p.ID
                   WHERE m.ID = :ID", new { ID = id });
        }
    }
}
