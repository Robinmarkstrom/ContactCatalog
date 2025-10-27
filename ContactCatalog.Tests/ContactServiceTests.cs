using Xunit;
using ContactCatalog.Models;
using ContactCatalog.Services;
using ContactCatalog.Interfaces;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;

namespace ContactCatalog.Tests

{
    public class ContactServiceTests
    {
        private readonly IContactService _service;

        public ContactServiceTests()
        {
            var repo = new ContactRepository();
            _service = new ContactService(repo);

            repo.Add(new Contact { Id = 1, Name = "Adam Eriksson", Email = "adam@test.se", Tags = new List<string> { "bror", "jobb" } });
            repo.Add(new Contact { Id = 2, Name = "Filip Svensson", Email = "filip@test.se", Tags = new List<string> { "gym" } });
            repo.Add(new Contact { Id = 3, Name = "Stina Eriksson", Email = "stina@test.se", Tags = new List<string> { "syster", "jobb" } });
        }
        [Fact]
        public void SearchByName_ShouldReturnMatchingContacts()
        {
            var result = _service.SearchByName("Adam");
            Assert.Single(result);
            Assert.Contains(result, c => c.Name == "Adam Eriksson");
        }

        [Fact]
        public void SearchByName_ShouldReturnResultsFromRepository()
        {
            var mockRepo = new Mock<IContactRepository>();
            var fakeContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Robin", Email = "robin@test.se" },
                new Contact { Id = 2, Name = "Sven", Email = "sven@test.se" }
            };

            mockRepo.Setup(r => r.GetAll()).Returns(fakeContacts);

            var service = new ContactService(mockRepo.Object);
            var result = service.SearchByName("ro").ToList();

            Assert.Single(result);
            Assert.Equal("Robin", result[0].Name);
        }

        [Fact]
        public void FilterByTag_ShouldReturnResultsFromRepository()
        {
            var mockRepo = new Mock<IContactRepository>();
            var fakeContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Robin", Email = "robin@test.se", Tags = new List<string> { "jobb" } },
                new Contact { Id = 2, Name = "Sven", Email = "sven@test.se", Tags = new List<string> { "gym" } }
            };

            mockRepo.Setup(r => r.GetAll()).Returns(fakeContacts);

            var service = new ContactService(mockRepo.Object);
            var result = service.FilterByTag("jobb").ToList();

            Assert.Single(result);
            Assert.Equal("Robin", result[0].Name);
        }

        [Fact]
        public void FilterByTag_ShouldReturnCorrectContacts()
        {
            var result = _service.FilterByTag("jobb");
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FilterByTag_ShouldBeCaseInsensitive()
        {
            var result = _service.FilterByTag("GYm");
            Assert.Equal(1, result.Count());
        }
    }
}