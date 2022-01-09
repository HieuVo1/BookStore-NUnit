using AutoMapper;
using Book_store.AutoMapper;
using Book_store.DTOs.Commons;
using Book_store.DTOs.Publishers.Requests;
using Book_store.Services.Loggers;
using Book_store.Services.Publishers;
using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Reponsitories.Publishers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreUnitTest
{
    public class PublisherServiceUnitTest
    {
        private static DbContextOptions<BookStoreDbContext> dbContextOptions = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStore")
                .Options;

        private BookStoreDbContext _context;

        private PublisherService _publisherService;

        private LoggerManager _loggerManager;

        private IMapper _mapper;

        private PublisherRepository _publisherRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new BookStoreDbContext(dbContextOptions);
            _context.Database.EnsureCreated();
            SeedDatabase();

            _loggerManager = new LoggerManager();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelProfile());
                cfg.AddProfile(new ViewModelToDomainProfile());
            });

            _mapper = new Mapper(config);

            _publisherRepository = new PublisherRepository(_context);

            _publisherService = new PublisherService(_publisherRepository, _loggerManager, _mapper);

        }

        [OneTimeTearDown]
        public void CleaUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test,Order(1)]
        public void GetAllPublisher()
        {
            var parameter = new QueryStringParameter();
            var result = _publisherService.GetAll(parameter);

            Assert.That(result.Data.Count, Is.EqualTo(3));
            Assert.AreEqual(result.Data.Count, 3);
        }

        [Test, Order(2)]
        public void GetPublisherById()
        {
            var result = _publisherService.GetByID(1);
            Assert.That(result.Data.Id,Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void GetPublisherById2()
        {
            var result = _publisherService.GetByID(99);
            Assert.That(result.Data, Is.Null);
        }

        [Test, Order(4)]
        public async Task CreatePublisher()
        {
            var publisherCreateRequqest = new PublisherCreateRequest()
            {
                Name = "Publisher 1"
            };

            var result = await _publisherService.CreateAsync(publisherCreateRequqest);

            Assert.That(result.Data.Name, Is.EqualTo("Publisher 1"));
            Assert.That(result.Data.Name, Does.StartWith("P"));
        }


        [Test, Order(5)]
        public void GetPublisherWithBooks()
        {
            var result = _publisherService.GetByIDWithBooks(1);
            Assert.That(result.Data.Books.Count, Is.EqualTo(0));
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name =" Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = " Publisher 2"
                },
                 new Publisher()
                {
                    Id = 3,
                    Name = " Publisher 3"
                },

            };

            _context.Publishers.AddRange(publishers);

            _context.SaveChanges();
        }
     
    }
}