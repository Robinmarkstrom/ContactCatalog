using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ContactCatalog.Utilities
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email && addr.Host.Contains(".");
            }
            catch { return false; }
        }
    }
}
