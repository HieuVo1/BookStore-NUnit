using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Authors
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public Author FindAuthorWithBooks(int authorId)
        {
            return _dbContext.Authors.Include(a => a.Book_Authors)
                                    .ThenInclude(ba => ba.Book)
                                    .FirstOrDefault(a => a.Id == authorId);
        }
    }
}
