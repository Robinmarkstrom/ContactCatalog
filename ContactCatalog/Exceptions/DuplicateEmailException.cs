﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public string Email { get; }
        public DuplicateEmailException(string email)
            : base($"Duplicate email: {email}")
        {
            Email = email;
        }
    }
}
