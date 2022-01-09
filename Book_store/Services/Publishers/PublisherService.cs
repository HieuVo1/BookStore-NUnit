using AutoMapper;
using Book_store.DTOs.Books.Responses;
using Book_store.DTOs.Commons;
using Book_store.DTOs.Publishers.Requests;
using Book_store.DTOs.Publishers.Responses;
using Book_store.Services.Loggers;
using Entities.Models;
using Reponsitories.Publishers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Book_store.Services.Publishers
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public PublisherService(IPublisherRepository publisherRepository, ILoggerManager loggerManager, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<APIResponse<PublisherResponse>> CreateAsync(PublisherCreateRequest request)
        {
            var publisher = _mapper.Map<Publisher>(request);

            _publisherRepository.Create(publisher);
            await _publisherRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Add new publisher with id: {publisher.Id}.");

            return new APISuccessResponse<PublisherResponse>(_mapper.Map<PublisherResponse>(publisher), HttpStatusCode.Created);
        }

        public async Task<APIResponse<int>> DeleteAsync(int publisherId)
        {
            var publisher = _publisherRepository.FindByID(publisherId);
            if (publisher == null)
            {
                _loggerManager.LogError($"Can not find publisher with id: {publisherId}.");
                return new APIErrorResponse<int>("Can not find this publisher!", HttpStatusCode.NotFound);
            }

            _publisherRepository.Delete(publisher);
            await _publisherRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Deleted publisher with id: {publisherId}.");

            return new APISuccessResponse<int>(publisherId);
        }

        public APIResponse<IList<PublisherResponse>> GetAll(QueryStringParameter parameters)
        {
            var publishers = _publisherRepository.FindAll();

            var publisherResult = PageList<Publisher>.ToPageList(
                publishers.AsQueryable(),
                parameters.PageNumber,
                parameters.PageSize
                );

            var metadata = new
            {
                publisherResult.TotalCount,
                publisherResult.PageSize,
                publisherResult.CurrentPage,
                publisherResult.TotalPages,
                publisherResult.HasNext,
                publisherResult.HasPrevious
            };

            _loggerManager.LogInfo($"Get all classes from database.");

            return new APISuccessResponse<IList<PublisherResponse>>(
                _mapper.Map<IList<PublisherResponse>>(publisherResult),
                metadata: metadata);

        }

        public APIResponse<PublisherResponse> GetByID(int publisherId)
        {
            var publisher = _publisherRepository.FindByID(publisherId);
            if (publisher == null)
            {
                _loggerManager.LogError($"Can not find publisher with id: {publisherId}.");
                return new APIErrorResponse<PublisherResponse>("Can not find this publisher!", HttpStatusCode.NotFound);
            }

            _loggerManager.LogInfo($"Get publisher with id: {publisherId}.");

            return new APISuccessResponse<PublisherResponse>(_mapper.Map<PublisherResponse>(publisher));
        }

        public APIResponse<PublisherWithBooksResponse> GetByIDWithBooks(int publisherId)
        {
            var publisher = _publisherRepository.FindPublisherWithBooks(publisherId);
            if (publisher == null)
            {
                _loggerManager.LogError($"Can not find publisher with id: {publisherId}.");
                return new APIErrorResponse<PublisherWithBooksResponse>("Can not find this publisher!", HttpStatusCode.NotFound);
            }

            var publisherWithBooks = _mapper.Map<PublisherWithBooksResponse>(publisher);

            publisherWithBooks.Books = _mapper.Map<List<BookWithAuthorsResponse>>(publisher.Books);


            _loggerManager.LogInfo($"Get publisher with id: {publisherId}.");

            return new APISuccessResponse<PublisherWithBooksResponse>(publisherWithBooks);
        }

        public async Task<APIResponse<PublisherResponse>> UpdateAsync(int publisherId, PublisherCreateRequest request)
        {
            var publisher = _publisherRepository.FindByID(publisherId);
            if (publisher == null)
            {
                _loggerManager.LogError($"Can not find publisher with id: {publisherId}.");
                return new APIErrorResponse<PublisherResponse>("Can not find this publisher!", HttpStatusCode.NotFound);
            }

            publisher.Name = request.Name ?? publisher.Name;

            _publisherRepository.Update(publisher);

            await _publisherRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Update publisher with id: {publisherId}.");

            return new APISuccessResponse<PublisherResponse>(_mapper.Map<PublisherResponse>(publisher));
        }
    }
}
