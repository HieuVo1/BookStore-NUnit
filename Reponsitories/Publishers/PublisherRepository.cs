using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Publishers
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public Publisher FindPublisherWithBooks(int publisherId)
        {
            return _dbContext.Publishers.Include(p => p.Books)
                                         .ThenInclude(b => b.Book_Authors)
                                         .ThenInclude(ba => ba.Author)
                                         .FirstOrDefault(p => p.Id == publisherId);
        }
    }
}
