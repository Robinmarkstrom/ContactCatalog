using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; // Sätter standardvärdet till en tom sträng istället för null för att undvika NullReferenceException
        public string Email { get; set; } = "";
        public List<string> Tags { get; set; } = new(); // Använder en list istället för array för att kunna modifiera den dynamiskt
    }
}
