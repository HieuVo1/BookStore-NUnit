using Reponsitories.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Wrappers
{
    public interface IRepositoryWrapper
    {
        IBookRepository Book { get; }

        Task SaveChangeAsync();
    }
}
