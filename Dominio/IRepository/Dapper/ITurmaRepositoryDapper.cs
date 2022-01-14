using Dominio.Endity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository.Dapper
{
    public interface ITurmaRepositoryDapper : IBaseRepositoryDapper<Turma>
    {
        public IEnumerable<Aluno> GetAlunos(Guid id);
    }
}
