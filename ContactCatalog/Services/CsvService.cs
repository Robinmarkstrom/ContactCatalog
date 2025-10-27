using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Interfaces;
using ContactCatalog.Models;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ContactCatalog.Services
{
    public class CsvService : ICsvService
    {
        private readonly IContactRepository _repository;
        private readonly ILogger<CsvService> _logger;

        public CsvService(IContactRepository repository, ILogger<CsvService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Contact> ImportCsv(string path)
        {
            var contacts = new List<Contact>();
            var lines = File.ReadAllLines(path).Skip(1);

            foreach (var line in lines)
            {
                try
                {
                    var parts = line.Split(',');
                    var id = int.Parse(parts[0]);
                    var name = parts[1];
                    var email = parts[2];
                    var tags = parts.Length > 3
                        ? parts[3].Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
                        : new List<string>();

                    var contact = new Contact { Id = id, Name = name, Email = email, Tags = tags };
                    _repository.Add(contact);
                    contacts.Add(contact);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error importing line: {Line}", line);
                }
            }
            return contacts;
        }
       
        public string ExportCsv(IEnumerable<Contact> contacts)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,Name,Email,Tags");

            foreach (var c in contacts)
            {
                var tags = string.Join('|', c.Tags);
                sb.AppendLine($"{c.Id},{c.Name},{c.Email},{tags}");
            }
            return sb.ToString();
        }
    }
}
