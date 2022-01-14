using Dominio.Endity;
using Escola_ExercicioBancoDeDados.DTO.QueryParametes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository.Dapper
{
    public interface IAlunoRepositoryDapper : IBaseRepositoryDapper<Aluno>
    {
        public abstract IEnumerable<Aluno> GetAllByParameters(AlunoQuery query);
    }
}
