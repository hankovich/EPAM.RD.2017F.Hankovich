using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    public class DuplicateUserException : UserException
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(string message) : base(message)
        {
        }

        public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
