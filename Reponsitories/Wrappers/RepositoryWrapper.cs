using Entities.Context;
using Reponsitories.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Wrappers
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BookStoreDbContext _dbContext;
        private BookRepository _bookRepository;

        public RepositoryWrapper(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
      
        public IBookRepository Book
        {
            get
            {
                _bookRepository ??= new BookRepository(_dbContext);
                return _bookRepository;
            }
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
