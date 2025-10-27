using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Interfaces;
using ContactCatalog.Models;
using ContactCatalog.Utilities;
using Microsoft.Extensions.Logging;
using System.IO;


namespace ContactCatalog
{
    public class Menu
    {
        private readonly IContactService _contactService;
        private readonly ICsvService _csvService;
        private readonly ILogger<Menu> _logger;

        public Menu(IContactService contactService, ICsvService csvService, ILogger<Menu> logger)
        {
            _contactService = contactService;
            _csvService = csvService;
            _logger = logger;
        }
        public void RunMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Contact Catalog ===");
                Console.WriteLine("1) Add");
                Console.WriteLine("2) List");
                Console.WriteLine("3) Search (Name Contains)");
                Console.WriteLine("4) Filter by tag");
                Console.WriteLine("5) Export CSV");
                Console.WriteLine("0) Exit");
                Console.Write("\nChoose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddContact(); break;
                    case "2": ListAllContacts(); break;
                    case "3": SearchContacts(); break;
                    case "4": FilterContacts(); break;
                    case "5": ExportCsv(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }
        private void AddContact()
        {
            try
            {
                int id;
                while (true)
                {
                    Console.Write("Id: ");
                    if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                        break;
                    Console.WriteLine("Invalid ID, must use a positive integer.");
                }

                Console.Write("Name: ");
                string name = (Console.ReadLine() ?? "").Trim();

                string email;
                while (true)
                {
                    Console.Write("Email: ");
                    email = (Console.ReadLine() ?? "").Trim();
                    if (EmailValidator.IsValidEmail(email))
                        break;
                    Console.WriteLine("Invalid email, try again.");
                }

                Console.Write("Tags (use comma if multiple): ");
                var tagsInput = Console.ReadLine() ?? "";
                var tags = tagsInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(t => t.Trim())
                                    .ToList();
                var contact = new Contact { Id = id, Name = name, Email = email, Tags = tags };
                _contactService.AddContact(contact);

                Console.WriteLine("\nAdded 1 contact to the catalog.");
                _logger.LogInformation(new EventId(1, "AddContact"), "Contact added: {Email}", email);

                System.Threading.Thread.Sleep(50);
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error entering contact.");
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
        }
        private void ListAllContacts()
        {
            var contacts = _contactService.GetAllContacts().ToList();

            if (!contacts.Any())
            {
                Console.WriteLine("\nNo contacts in the catalog.");
            }
            else
            {
                Console.WriteLine("\nAll contacts:");
                foreach (var c in contacts)
                {
                    Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(',', c.Tags)}]");
                }
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
        private void SearchContacts()
        {
            Console.Write("Search by name: ");
            string term = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrEmpty(term))
            {
                Console.WriteLine("No search term specified.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
                return;
            }

            var results = _contactService.SearchByName(term).ToList();

            if (!results.Any())
            {
                Console.WriteLine($"\nNo contacts found by '{term}'.");
            }
            else
            {
                Console.WriteLine($"\nMatches for '{term}:' ");
                foreach (var c in results)
                {
                    Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(',', c.Tags)}]");
                }
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private void FilterContacts()
        {
            Console.Write("Tag: ");
            string tag = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrEmpty(tag))
            {
                Console.WriteLine("No tag specified.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
                return;
            }

            var results = _contactService.FilterByTag(tag).ToList();

            if (!results.Any())
            {
                Console.WriteLine($"\nNo contacts with tag {tag} was found.");
            }
            else
            {
                Console.WriteLine($"\nContacts with tag: '{tag}'");
                foreach (var c in results)
                {
                    Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(',', c.Tags)}]");
                }
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private void ExportCsv()
        {
            var contacts = _contactService.GetAllContacts().ToList();

            if (!contacts.Any())
            {
                Console.WriteLine("\nNo contacts to export.");
            }
            else
            {
                string filePath = "contacts.csv";
                var csv = _csvService.ExportCsv(contacts);
                File.WriteAllText(filePath, csv);

                Console.WriteLine($"\nExportFile: {filePath}");
                Console.WriteLine($"[Export done] {contacts.Count} contact(s) exported.");
                _logger.LogInformation("Export done: {Count} contacts till {path}", contacts.Count, filePath);
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
