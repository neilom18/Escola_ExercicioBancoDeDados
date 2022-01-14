using Dominio.Endity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository.Dapper
{
    public interface IBaseRepositoryDapper<TEndity> where TEndity : Base
    {
        public TEndity Get(Guid id);
    }
}
