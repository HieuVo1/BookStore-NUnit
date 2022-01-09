using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories.Publishers
{
    public interface IPublisherRepository:IRepositoryBase<Publisher>
    {
        public Publisher FindPublisherWithBooks(int publisherId);
    }
}
