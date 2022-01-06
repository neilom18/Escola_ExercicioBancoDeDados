using Dominio.IRepository.EF;
using Escola_ExercicioBancoDeDados.Endity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext.Repository
{
    public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepositoryEF
    {
        public AlunoRepository(AppContext context) : base(context)
        {
        }
    }
}
