using ContactCatalog.Interfaces;
using ContactCatalog.Services;
using ContactCatalog.CommandLine;
using Microsoft.Extensions.Logging;


namespace ContactCatalog
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repository = new ContactRepository();
            var contactService = new ContactService(repository);

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger<Menu> menuLogger = loggerFactory.CreateLogger<Menu>();
            ILogger<CsvService> csvLogger = loggerFactory.CreateLogger<CsvService>();
            var csvService = new CsvService(repository, csvLogger);

            CommandLineHandler.HandleArgs(csvService, csvLogger);

            var menu = new Menu(contactService, csvService, menuLogger);
            menu.RunMenu();
        }
    }
}
