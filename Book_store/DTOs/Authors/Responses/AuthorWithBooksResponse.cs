using Book_store.DTOs.Books.Responses;
using System.Collections.Generic;

namespace Book_store.DTOs.Authors.Responses
{
    public class AuthorWithBooksResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookResponse> Books { get; set; }
    }
}
