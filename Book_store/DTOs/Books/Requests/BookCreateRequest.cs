using System;
using System.Collections.Generic;

namespace Book_store.DTOs.Books.Requests
{
    public class BookCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int Rate { get; set; }
        public string CoverPictureUrl { get; set; }
        public DateTime DateAdd { get; set; }
        public string Genre { get; set; }
        public int publisherId { get; set; }
        public List<int> authorIds { get; set; }
    }
}
