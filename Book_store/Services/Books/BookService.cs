using AutoMapper;
using Book_store.DTOs.Authors.Responses;
using Book_store.DTOs.Books.Requests;
using Book_store.DTOs.Books.Responses;
using Book_store.DTOs.Commons;
using Book_store.Services.Loggers;
using Entities.Context;
using Entities.Models;
using Reponsitories.Books;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Book_store.Services.Books
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, ILoggerManager loggerManager, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<APIResponse<BookResponse>> CreateAsync(BookCreateRequest request)
        {
            var newBook = _mapper.Map<Book>(request);

            newBook.Book_Authors = new List<Book_Author>();

            foreach (var authorId in request.authorIds)
            {
                newBook.Book_Authors.Add(new Book_Author()
                {
                    AuthorId = authorId,
                });
            }
            _bookRepository.Create(newBook);

            await _bookRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Add new class with id: {newBook.Id}.");
            return new APISuccessResponse<BookResponse>(_mapper.Map<BookResponse>(newBook), HttpStatusCode.Created);
        }

        public async Task<APIResponse<int>> DeleteAsync(int bookId)
        {
            var book = _bookRepository.FindByID(bookId);

            if (book == null)
            {
                _loggerManager.LogError($"Book with id: {bookId}, hasn't been found in db.");
                return new APIErrorResponse<int>("Can not find this book!", HttpStatusCode.NotFound);
            }

            _bookRepository.Delete(book);

            await _bookRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Deleted book with id: {book.Id}.");
            return new APISuccessResponse<int>(book.Id);
        }

        public APIResponse<IList<BookResponse>> GetAll(QueryStringParameter parameters)
        {
            var books = _bookRepository.FindAll();

            var booksResult = PageList<Book>.ToPageList(
                books.AsQueryable(),
                parameters.PageNumber,
                parameters.PageSize);

            var metadata = new
            {
                booksResult.TotalCount,
                booksResult.PageSize,
                booksResult.CurrentPage,
                booksResult.TotalPages,
                booksResult.HasNext,
                booksResult.HasPrevious
            };

            _loggerManager.LogInfo($"Get all classes from database.");

            return new APISuccessResponse<IList<BookResponse>>(
                _mapper.Map<IList<BookResponse>>(booksResult),
                metadata: metadata
                );
        }

        public APIResponse<BookResponse> GetByID(int bookId)
        {
            var book = _bookRepository.FindByID(bookId);

            if (book == null)
            {
                _loggerManager.LogError($"Book with id: {bookId}, hasn't been found in db.");
                return new APIErrorResponse<BookResponse>("Can not find this class!", HttpStatusCode.NotFound);
            }

            _loggerManager.LogInfo($"Get book with id: {bookId}");
            return new APISuccessResponse<BookResponse>(_mapper.Map<BookResponse>(book));
        }

        public APIResponse<BookWithAuthorsResponse> GetByIDWithAuthors(int bookId)
        {
            var book = _bookRepository.FindBookWithAuthors(bookId);

            if (book == null)
            {
                _loggerManager.LogError($"Book with id: {bookId}, hasn't been found in db.");
                return new APIErrorResponse<BookWithAuthorsResponse>("Can not find this class!", HttpStatusCode.NotFound);
            }

           var bookWithAuthors = _mapper.Map<BookWithAuthorsResponse>(book);
            bookWithAuthors.Authors = _mapper.Map<List<AuthorResponse>>(book.Book_Authors?.Select(x => x.Author).ToList());

            _loggerManager.LogInfo($"Get book with id: {bookId}");
            return new APISuccessResponse<BookWithAuthorsResponse>(bookWithAuthors);
        }

        public async Task<APIResponse<BookResponse>> UpdateAsync(int bookId, BookCreateRequest request)
        {
            var book = _bookRepository.FindByID(bookId);

            if (book == null)
            {
                _loggerManager.LogError($"Book with id: {bookId}, hasn't been found in db.");
                return new APIErrorResponse<BookResponse>("Can not find this book!", HttpStatusCode.NotFound);
            }

            book.Title = request.Title ?? book.Title;
            book.Description = request.Description ?? book.Description;
            book.Genre = request.Genre ?? book.Genre;
            book.Rate = request.Rate;
            book.CoverPictureUrl = request.CoverPictureUrl ?? book.CoverPictureUrl;
            book.PublisherId = request.publisherId == 0 ? book.PublisherId: request.publisherId;

            _bookRepository.Update(book);
            await _bookRepository.SaveChangeAsync();

            _loggerManager.LogInfo($"Updated class with id: {bookId}.");
            return new APISuccessResponse<BookResponse>(_mapper.Map<BookResponse>(book));
        }
    }
}
