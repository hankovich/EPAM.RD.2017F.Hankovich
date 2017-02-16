using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Generator
{
    /// <summary>
    /// Represents concrete realization of <see cref="IIdGenerator"/>
    /// </summary>
    public class IdGenerator : IIdGenerator
    {
        /// <summary>
        /// Generate id for user
        /// </summary>
        /// <param name="user">User to generate id for</param>
        /// <returns>Id for user</returns>
        public int GenerateId(User user)
        {
            return RsHash(user.FirstName + user.LastName + user.DateOfBirth);
        }

        private static int RsHash(string str)
        {
            const int B = 378551;
            int a = 63689;
            int hash = 0;

            foreach (char t in str)
            {
                hash *= a;
                hash += t;
                a *= B;
            }

            return hash;
        }
    }
}
