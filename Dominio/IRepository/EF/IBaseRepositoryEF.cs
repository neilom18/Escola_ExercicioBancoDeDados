using Dominio.Endity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository.EF
{
    public interface IBaseRepositoryEF<TEndity> where TEndity : Base
    {
        public void Insert(TEndity entity);
        public void Update(TEndity entity);
        public void Delete(TEndity endity);
    }
}
