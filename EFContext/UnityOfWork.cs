using System;
using Dominio;
using Dominio.IRepository.Dapper;
using Dominio.IRepository.EF;
using EFContext.Repository;

namespace EFContext
{
    public class UnityOfWork : IUnityOfWork
    {
        private bool _disposedValue;
        private readonly AppContext _appContext;

        public IAlunoRepositoryEF AlunoRepositoryEF { get; set; }
        public IProfessorRepositoryEF ProfessorRepositoryEF { get; set; }
        public IMateriaRepositoryEF MateriaRepositoryEF { get; set; }
        public ICursoRepositoryEF CursoRepositoryEF { get; set; }
        public ITurmaRepositoryEF TurmaRepositoryEF { get; set; }

        public UnityOfWork(AppContext appContext)
        {
            _appContext = appContext;

            AlunoRepositoryEF = new AlunoRepository(appContext);
            ProfessorRepositoryEF = new ProfessorRepository(appContext);
            MateriaRepositoryEF = new MateriaRepository(appContext);
            CursoRepositoryEF = new CursoRepository(appContext);
            TurmaRepositoryEF = new TurmaRepository(appContext);
        }

        public bool Commit()
        {
            try
            {
                _appContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _appContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
 }
