using Book_store.DTOs.Authors.Responses;
using System;
using System.Collections.Generic;

namespace Book_store.DTOs.Books.Responses
{
    public class BookWithAuthorsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int Rate { get; set; }
        public string CoverPictureUrl { get; set; }
        public DateTime DateAdd { get; set; }
        public int PublisherId { get; set; }
        public List<AuthorResponse> Authors { get; set; }
    }
}
