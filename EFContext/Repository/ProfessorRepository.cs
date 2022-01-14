using Dominio.Endity;
using Dominio.IRepository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext.Repository
{
    public class ProfessorRepository : BaseRepository<Professor>, IProfessorRepositoryEF
    {
        public ProfessorRepository(Context context) : base(context)
        {
        }
    }
}
