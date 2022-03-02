using contracts.Repositories;
using entities.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private IUserRepository _userRepository;
        public RepositoryManager(DataContext context)
        {
            context = _context;
        }

        public IUserRepository UserRepository => _userRepository ??= new EfUserRepository(_context);
    }
}
