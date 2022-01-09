using AutoMapper;
using Book_store.DTOs.Authors.Requests;
using Book_store.DTOs.Authors.Responses;
using Book_store.DTOs.Books.Responses;
using Book_store.DTOs.Commons;
using Book_store.Services.Loggers;
using Entities.Models;
using Reponsitories.Authors;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Book_store.Services.Authors
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, ILoggerManager loggerManager, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<APIResponse<AuthorResponse>> CreateAsync(AuthorCreateRequest request)
        {
            var author = _mapper.Map<Author>(request);

            _authorRepository.Create(author);
            await _authorRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Add new author with id: {author.Id}.");

            return new APISuccessResponse<AuthorResponse>(_mapper.Map<AuthorResponse>(author), HttpStatusCode.Created);
        }

        public async Task<APIResponse<int>> DeleteAsync(int authorId)
        {
            var author = _authorRepository.FindByID(authorId);
            if (author == null)
            {
                _loggerManager.LogError($"Can not find author with id: {authorId}.");
                return new APIErrorResponse<int>("Can not find this author!", HttpStatusCode.NotFound);
            }

            _authorRepository.Delete(author);
            await _authorRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Deleted author with id: {authorId}.");

            return new APISuccessResponse<int>(authorId);
        }

        public APIResponse<IList<AuthorResponse>> GetAll(QueryStringParameter parameters)
        {
            var authors = _authorRepository.FindAll();

            var authorResult = PageList<Author>.ToPageList(
                authors.AsQueryable(),
                parameters.PageNumber,
                parameters.PageSize
                );

            var metadata = new
            {
                authorResult.TotalCount,
                authorResult.PageSize,
                authorResult.CurrentPage,
                authorResult.TotalPages,
                authorResult.HasNext,
                authorResult.HasPrevious
            };

            _loggerManager.LogInfo($"Get all classes from database.");

            return new APISuccessResponse<IList<AuthorResponse>>(
                _mapper.Map<IList<AuthorResponse>>(authorResult),
                metadata: metadata);

        }

        public APIResponse<AuthorResponse> GetByID(int authorId)
        {
            var author = _authorRepository.FindByID(authorId);
            if (author == null)
            {
                _loggerManager.LogError($"Can not find author with id: {authorId}.");
                return new APIErrorResponse<AuthorResponse>("Can not find this author!", HttpStatusCode.NotFound);
            }
     
            _loggerManager.LogInfo($"Get author with id: {authorId}.");

            return new APISuccessResponse<AuthorResponse>(_mapper.Map<AuthorResponse>(author));
        }

        public APIResponse<AuthorWithBooksResponse> GetByIDWithBooks(int authorId)
        {
            var author = _authorRepository.FindAuthorWithBooks(authorId);
            if (author == null)
            {
                _loggerManager.LogError($"Can not find author with id: {authorId}.");
                return new APIErrorResponse<AuthorWithBooksResponse>("Can not find this author!", HttpStatusCode.NotFound);
            }

            var authorWithBooks = _mapper.Map<AuthorWithBooksResponse>(author);

            authorWithBooks.Books = _mapper.Map<List<BookResponse>>(author.Book_Authors.Select(a => a.Book).ToList());

            _loggerManager.LogInfo($"Get author with id: {authorId}.");

            return new APISuccessResponse<AuthorWithBooksResponse>(authorWithBooks);
        }

        public async Task<APIResponse<AuthorResponse>> UpdateAsync(int authorId, AuthorCreateRequest request)
        {
            var author = _authorRepository.FindByID(authorId);
            if (author == null)
            {
                _loggerManager.LogError($"Can not find author with id: {authorId}.");
                return new APIErrorResponse<AuthorResponse>("Can not find this author!", HttpStatusCode.NotFound);
            }

            author.Name = request.Name ?? author.Name;

            _authorRepository.Update(author);

            await _authorRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Update author with id: {authorId}.");

            return new APISuccessResponse<AuthorResponse>(_mapper.Map<AuthorResponse>(author));
        }
    }
}
