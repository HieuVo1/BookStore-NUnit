using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Author:BaseModel
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        //Navigation Properties
        public List<Book_Author> Book_Authors { get; set; }
    }
}
