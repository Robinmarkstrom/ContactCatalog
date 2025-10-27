using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ContactCatalog.Services;
using ContactCatalog.Models;
using ContactCatalog.Exceptions;


namespace ContactCatalog.Tests
{
    public class ContactRepositoryTests
    {
        [Fact]
        public void Add_ShouldThrow_WhenEmailInvalid()
        {
            var repo = new ContactRepository();
            var contact = new Contact { Id = 1, Name = "Wrong", Email = "No email" };

            Assert.Throws<InvalidEmailException>(() => repo.Add(contact));
        }

        [Fact]
        public void Add_ShouldThrow_WhenDuplicateEmail()
        {
            var repo = new ContactRepository();
            var contact1 = new Contact { Id = 1, Name = "Adam", Email = "123@test.se" };
            var contact2 = new Contact { Id = 2, Name = "Filip", Email = "123@test.se" };

            repo.Add(contact1);
            Assert.Throws<DuplicateEmailException>(() => repo.Add(contact2));
        }

        [Fact]
        public void Add_ShouldStoreContact_WhenValid()
        {
            var repo = new ContactRepository();
            var contact = new Contact { Id = 1, Name = "Test", Email = "test@test.se" };

            repo.Add(contact);

            var all = repo.GetAll();
            Assert.Single(all);
            Assert.Contains(all, x => x.Email == "test@test.se");
        }
    }
}
