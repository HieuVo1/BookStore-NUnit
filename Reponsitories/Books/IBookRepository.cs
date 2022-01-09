using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Books
{
    public interface IBookRepository: IRepositoryBase<Book>
    {
       public Book FindBookWithAuthors(int bookId);
    }
}
