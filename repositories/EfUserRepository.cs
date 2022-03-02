using contracts.Repositories;
using entities.DataContexts;
using entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repositories
{
    public class EfUserRepository:RepositoryBase<User>,IUserRepository
    {
        public EfUserRepository(DataContext context) : base(context)
        {

        }
    }
}
