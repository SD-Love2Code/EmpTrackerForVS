using System;
using System.Collections.Generic;

using EmployeeTracker.DataAccessLayer.Exceptions;
using EmployeeTracker.DataAccessLayer.Implementation.Common;

namespace EmployeeTracker.DataAccessLayer.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        #region Private Member Variables

        private static readonly Dictionary<Type, Func<IRepositoryConfiguration, IRepository>> _repositories = new Dictionary<Type, Func<IRepositoryConfiguration, IRepository>>()
        {
            // register the ach repositories
            //{ typeof(EmployeeTracker.DataAccessLayer.Repositories.Ach.IArchiveRepository), (c) => new EmployeeTracker.DataAccessLayer.Implementation.Ach.ArchiveRepository(c.Connection) },
            //{ typeof(EmployeeTracker.DataAccessLayer.Repositories.Ach.IBatchRepository), (c) => new EmployeeTracker.DataAccessLayer.Implementation.Ach.BatchRepository(c.Connection) },
           

        };

        private readonly IRepositoryConfiguration _configuration;

        #endregion

        #region Public Constructors

        public RepositoryFactory(IRepositoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region IRepositoryFactory Members

        public T Get<T>() where T : class, IRepository
        {
            T result;
            Func<IRepositoryConfiguration, IRepository> function;
            if ((result = _repositories.TryGetValue(typeof(T), out function) ? function(_configuration) as T : default(T)) == null)
                throw new DalException(string.Format("Unable to resolve factory interface {0}", typeof(T).FullName), DalError.FactoryRegistration, null);
            return result;
        }

        #endregion

        #region RepositoryFactory Members

        //public ITypeNameRepository GetTypeNameRepository()
        //{
        //    return new TypeNameRepository(_configuration.Connection);
        //}

        #endregion
    }
}