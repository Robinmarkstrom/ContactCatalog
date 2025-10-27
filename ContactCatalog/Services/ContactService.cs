using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Models;
using ContactCatalog.Interfaces;

namespace ContactCatalog.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public void AddContact(Contact contact) => _repository.Add(contact);

        public IEnumerable<Contact> GetAllContacts() => _repository.GetAll().OrderBy(c => c.Name);

        public IEnumerable<Contact> SearchByName(string term)
        {
            return _repository.GetAll()
                .Where(c => c.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);
        }

        public IEnumerable<Contact> FilterByTag(string tag)
        {
            return _repository.GetAll()
                .Where(c => c.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);
        }
    }
}
