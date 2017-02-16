using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    /// <summary>
    /// Class to process information about users with incomplete profiles
    /// </summary>
    public class IncompleteProfileException : UserException
    {
        /// <summary>
        /// Create new intance of <see cref="IncompleteProfileException"/>
        /// </summary>
        public IncompleteProfileException()
        {
        }

        /// <summary>
        /// Create new intance of <see cref="IncompleteProfileException"/>
        /// </summary>
        /// <param name="message"></param>
        public IncompleteProfileException(string message) : base(message)
        {
        }

        /// <summary>
        /// Create new intance of <see cref="IncompleteProfileException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">Information about inner exception</param>
        public IncompleteProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
