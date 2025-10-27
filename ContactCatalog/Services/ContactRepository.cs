using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Interfaces;
using ContactCatalog.Models;
using ContactCatalog.Exceptions;
using ContactCatalog.Utilities;

namespace ContactCatalog.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contacts = new();
        private readonly HashSet<string> _emails = new(StringComparer.OrdinalIgnoreCase);

        public void Add(Contact contact)
        {
            if (!EmailValidator.IsValidEmail(contact.Email)) throw new InvalidEmailException(contact.Email);
            if (!_emails.Add(contact.Email)) throw new DuplicateEmailException(contact.Email);

            _contacts.Add(contact.Id, contact);
        }

        public IEnumerable<Contact> GetAll() => _contacts.Values;
    }
}
