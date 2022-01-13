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
    public class ProfessorRepository : BaseRepository<Professor>, IProfessorRepositoryDapper
    {
        public ProfessorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override Professor Get(Guid id)
        {
            return _connection.QuerySingle<Professor>(@"SELECT NOME, IDADE, ID
                                                      FROM PROFESSOR
                                                    WHERE ID = :ID", new { ID = id});
        }
    }
}
