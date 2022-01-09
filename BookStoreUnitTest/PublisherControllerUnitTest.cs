using AutoMapper;
using Book_store.AutoMapper;
using Book_store.Controllers;
using Book_store.DTOs.Commons;
using Book_store.DTOs.Publishers.Requests;
using Book_store.DTOs.Publishers.Responses;
using Book_store.Services.Loggers;
using Book_store.Services.Publishers;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Reponsitories.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreUnitTest
{
    public class PublisherControllerUnitTest
    {
        private static DbContextOptions<BookStoreDbContext> dbContextOptions = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStore")
                .Options;

        private BookStoreDbContext _context;

        PublishersController _publishersController;

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

            _publishersController = new PublishersController(_publisherService, _loggerManager);
        }


        [Test,Order(1)]
        public void HttpGet_GetAllPublishers_ReturnOK()
        {
            QueryStringParameter parameter = new QueryStringParameter();
            IActionResult actionResult = _publishersController.GetAll(parameter);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as APIResponse<IList<PublisherResponse>>;

            Assert.That(actionResultData.Data.First().Name, Is.EqualTo("Publisher 1"));

        }

        [Test, Order(2)]
        public async Task HttpPost_AddPublisher_ReturnOK()
        {

            var publisher = new PublisherCreateRequest()
            {
                Name = "Publisher 4"
            };

            IActionResult actionResult = await _publishersController.CreateAsync(publisher);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as APIResponse<PublisherResponse>;

            Assert.That(actionResultData.Data.Name, Is.EqualTo("Publisher 4"));

        }


        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 5,
                    Name ="Publisher 1"
                },
                new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 2"
                },
                 new Publisher()
                {
                    Id = 8,
                    Name = "Publisher 3"
                },

            };

            _context.Publishers.AddRange(publishers);

            _context.SaveChanges();
        }
    }
}
