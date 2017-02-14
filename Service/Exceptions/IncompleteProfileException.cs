using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    public class IncompleteProfileException: UserException
    {
        public IncompleteProfileException()
        {
        }

        public IncompleteProfileException(string message) : base(message)
        {
        }

        public IncompleteProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
