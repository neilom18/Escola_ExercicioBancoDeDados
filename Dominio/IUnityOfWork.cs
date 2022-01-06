using Dominio.IRepository;
using Dominio.IRepository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public interface IUnityOfWork : IDisposable
    {
        IAlunoRepositoryEF AlunoRepositoryEF { get; }
        IProfessorRepositoryEF ProfessorRepositoryEF { get; }
        IMateriaRepositoryEF MateriaRepositoryEF { get; }
        ICursoRepositoryEF CursoRepositoryEF { get; }
        ITurmaRepositoryEF TurmaRepositoryEF { get; }

        public bool Commit();
    }
}
