using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Authors
{
    public interface IAuthorRepository:IRepositoryBase<Author> 
    {
        public Author FindAuthorWithBooks(int authorId);
    }
}
