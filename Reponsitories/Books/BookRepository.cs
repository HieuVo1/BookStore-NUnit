using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Books
{
    public class BookRepository: RepositoryBase<Book>, IBookRepository
    {
        
        public BookRepository(BookStoreDbContext dbContext):base(dbContext)
        {

        }

        public Book FindBookWithAuthors(int bookId)
        {
            return _dbContext.Books.Include(b => b.Book_Authors)
                .ThenInclude(ba=> ba.Author).FirstOrDefault(b => b.Id == bookId);
        }
    }
}
