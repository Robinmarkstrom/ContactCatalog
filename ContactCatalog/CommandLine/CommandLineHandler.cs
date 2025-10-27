using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ContactCatalog.Services;
using Microsoft.Extensions.Logging;

namespace ContactCatalog.CommandLine
{
    public static class CommandLineHandler
    {
        public static void HandleArgs(CsvService csvService, ILogger<CsvService> logger)
        {
            var argsList = Environment.GetCommandLineArgs();
            var importIndex = Array.IndexOf(argsList, "--import");

            if (importIndex >= 0 && importIndex + 1 < argsList.Length)
            {
                var path = argsList[importIndex + 1];

                try
                {
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"File {path} does not exist.");
                        return;
                    }

                    var importedContacts = csvService.ImportCsv(path);
                    Console.WriteLine($"\nImported {importedContacts.Count()} contacts from {path}");
                    logger.LogInformation("Contacts imported from {Path}", path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error importing CSV: {ex.Message}");
                    logger.LogError(ex, "Could not import {Path}", path);
                }
            }
            else if (argsList.Length > 1)
            {
                Console.WriteLine("Invalid argument. Use: --import path.csv");
            }
        }
    }
}
