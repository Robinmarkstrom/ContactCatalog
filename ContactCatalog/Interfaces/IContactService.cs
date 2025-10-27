using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Models;

namespace ContactCatalog.Interfaces
{
    public interface IContactService
    {
        void AddContact(Contact contact);
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<Contact> SearchByName(string term); // term är sökordet, inte det exakta namnet
        IEnumerable<Contact> FilterByTag(string tag);
    }
}
