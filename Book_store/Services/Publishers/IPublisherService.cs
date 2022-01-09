using Book_store.DTOs.Commons;
using Book_store.DTOs.Publishers.Requests;
using Book_store.DTOs.Publishers.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_store.Services.Publishers
{
    public interface IPublisherService
    {
        public Task<APIResponse<PublisherResponse>> CreateAsync(PublisherCreateRequest request);
        public APIResponse<IList<PublisherResponse>> GetAll(QueryStringParameter parameters);
        public APIResponse<PublisherResponse> GetByID(int publisherId);
        public APIResponse<PublisherWithBooksResponse> GetByIDWithBooks(int publisherId);
        public Task<APIResponse<PublisherResponse>> UpdateAsync(int publisherId, PublisherCreateRequest request);
        public Task<APIResponse<int>> DeleteAsync(int publisherId);
    }
}
