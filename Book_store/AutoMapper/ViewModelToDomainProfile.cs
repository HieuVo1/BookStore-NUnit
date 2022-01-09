using AutoMapper;
using Book_store.DTOs.Authors.Requests;
using Book_store.DTOs.Books.Requests;
using Book_store.DTOs.Publishers.Requests;
using Entities.Models;

namespace Book_store.AutoMapper
{
    public class ViewModelToDomainProfile: Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<BookCreateRequest, Book>();
            CreateMap<AuthorCreateRequest, Author>();
            CreateMap<PublisherCreateRequest, Publisher>();
        }
    }
}
