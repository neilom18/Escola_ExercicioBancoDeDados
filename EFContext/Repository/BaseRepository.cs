using Dominio.Endity;
using Dominio.IRepository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext.Repository
{
    public class BaseRepository<TEntity> : IBaseRepositoryEF<TEntity> where TEntity : Base
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

    }
}
