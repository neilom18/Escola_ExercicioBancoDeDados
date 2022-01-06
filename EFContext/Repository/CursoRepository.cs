using Dominio.IRepository.EF;
using Escola_ExercicioBancoDeDados.Endity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext.Repository
{
    public class CursoRepository : BaseRepository<Curso>, ICursoRepositoryEF
    {
        public CursoRepository(AppContext context) : base(context)
        {
        }
    }
}
