using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Models;

namespace ContactCatalog.Interfaces
{
    public interface ICsvService
    {
        IEnumerable<Contact> ImportCsv(string path); // path är filens sökväg
        string ExportCsv(IEnumerable<Contact> contacts);
    }
}
