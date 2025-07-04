using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Contract.ICommon
{
    public interface IUnitOfWork
    {
        //IBaseRepository<Employee> Employees { get; }

        IBaseRepository<T> GetRepository<T>() where T : Entity;
        Task SaveAsync();

    }
}
