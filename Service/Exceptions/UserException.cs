using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    /// <summary>
    /// Head of custom UserException's hierarchy
    /// </summary>
    public class UserException : Exception
    {
        /// <summary>
        /// Create new intance of <see cref="UserException"/>
        /// </summary>
        public UserException()
        {
        }

        /// <summary>
        /// Create new intance of <see cref="UserException"/>
        /// </summary>
        /// <param name="message">Information about exception</param>
        public UserException(string message) : base(message)
        {
        }

        /// <summary>
        /// Create new intance of <see cref="UserException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">Information about inner exception</param>
        public UserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
