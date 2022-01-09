using Book_store.DTOs.Books.Responses;
using System.Collections.Generic;

namespace Book_store.DTOs.Publishers.Responses
{
    public class PublisherWithBooksResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookWithAuthorsResponse> Books { get; set; }
    }
}
