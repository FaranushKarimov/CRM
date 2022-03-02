using entities.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.Repositories
{
    public interface IRepositoryManager
    {
       IUserRepository UserRepository { get; }
    }
}
