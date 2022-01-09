using AutoMapper;
using Book_store.DTOs.Authors.Responses;
using Book_store.DTOs.Books.Responses;
using Book_store.DTOs.Publishers.Responses;
using Entities.Models;

namespace Book_store.AutoMapper
{
    public class DomainToViewModelProfile:Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Book, BookResponse>();
            CreateMap<Book, BookWithAuthorsResponse>();
            CreateMap<Author, AuthorResponse>();
            CreateMap<Author, AuthorWithBooksResponse>();
            CreateMap<Publisher, PublisherResponse>();
            CreateMap<Publisher, PublisherWithBooksResponse>();
        }
    }
}
