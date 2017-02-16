using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Generator
{
    /// <summary>
    /// Represents method to generate id for user
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Generate id for user
        /// </summary>
        /// <param name="user">User to generate id for</param>
        /// <returns>Id for user</returns>
        int GenerateId(User user);
    }
}
