using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ContactCatalog.Utilities;

namespace ContactCatalog.Tests
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("invalid", false)]
        [InlineData("user@domain", false)]
        [InlineData("user@domain.com", true)]
        public void IsValidEmail_ShouldValidateCorrectly(string email, bool expected)
        {
            var result = EmailValidator.IsValidEmail(email);
            Assert.Equal(expected, result);
        }
    }
}
