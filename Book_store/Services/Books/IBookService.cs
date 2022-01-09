using Book_store.DTOs.Books.Requests;
using Book_store.DTOs.Books.Responses;
using Book_store.DTOs.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_store.Services.Books
{
    public interface IBookService
    {
        public Task<APIResponse<BookResponse>> CreateAsync(BookCreateRequest request);
        public APIResponse<IList<BookResponse>> GetAll(QueryStringParameter parameters);
        public APIResponse<BookResponse> GetByID(int bookId);
        public APIResponse<BookWithAuthorsResponse> GetByIDWithAuthors(int bookId);
        public Task<APIResponse<BookResponse>> UpdateAsync(int bookId, BookCreateRequest request);
        public Task<APIResponse<int>> DeleteAsync(int bookId);
    }
}
