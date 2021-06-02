using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class UserException : Exception
    {
        public UserException(string message = "User is not found")
            : base(message) { }
    }
    public class NonExistenceException : Exception
    {
        public NonExistenceException(string message)
            : base(message) { }
    }
}
