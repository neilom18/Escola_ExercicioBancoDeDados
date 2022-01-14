using Dominio.Endity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository.Dapper
{
    public interface ICursoRepositoryDapper : IBaseRepositoryDapper<Curso>
    {
        public IEnumerable<Materia> GetMateriasFromCursos(Guid id);
    }
}
