using Dapper;
using Dominio.IRepository.Dapper;
using Escola_ExercicioBancoDeDados.Endity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperContext.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepositoryDapper<TEntity> where TEntity : Base
    {
        protected readonly OracleConnection _connection;
        public BaseRepository(IConfiguration configuration)
        {
            _connection = new OracleConnection(configuration.GetConnectionString("APPACADEMY"));
        }
        public abstract TEntity Get(Guid id);
    }
}
