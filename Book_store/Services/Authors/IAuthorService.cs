using Book_store.DTOs.Authors.Requests;
using Book_store.DTOs.Authors.Responses;
using Book_store.DTOs.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_store.Services.Authors
{
    public interface IAuthorService
    {
        public Task<APIResponse<AuthorResponse>> CreateAsync(AuthorCreateRequest request);
        public APIResponse<IList<AuthorResponse>> GetAll(QueryStringParameter parameters);
        public APIResponse<AuthorResponse> GetByID(int authorId);
        public APIResponse<AuthorWithBooksResponse> GetByIDWithBooks(int authorId);
        public Task<APIResponse<AuthorResponse>> UpdateAsync(int authorId, AuthorCreateRequest request);
        public Task<APIResponse<int>> DeleteAsync(int authorId);
    }
}
