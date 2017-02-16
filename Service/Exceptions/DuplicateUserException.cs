using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    /// <summary>
    /// Class to process information about duplicate users
    /// </summary>
    public class DuplicateUserException : UserException
    {
        /// <summary>
        /// Create new intance of <see cref="DuplicateUserException"/>
        /// </summary>
        public DuplicateUserException()
        {
        }

        /// <summary>
        /// Create new intance of <see cref="DuplicateUserException"/>
        /// </summary>
        /// <param name="message"></param>
        public DuplicateUserException(string message) : base(message)
        {
        }

        /// <summary>
        /// Create new intance of <see cref="DuplicateUserException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">Information about inner exception</param>
        public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
