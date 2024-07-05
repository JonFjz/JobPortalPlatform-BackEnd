using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IJppRepository<TEntity> Repository<TEntity>() where TEntity : class;

        bool Complete();
    }
}
