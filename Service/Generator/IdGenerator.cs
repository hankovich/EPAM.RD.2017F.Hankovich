using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Generator
{
    public class IdGenerator: IIdGenerator
    {
        public int GenerateId(User user)
        {
            return RsHash(user.FirstName + user.LastName + user.DateOfBirth);
        }

        private static int RsHash(string str)
        {
            const int b = 378551;
            int a = 63689;
            int hash = 0;

            foreach (char t in str)
            {
                hash = hash * a + t;
                a *= b;
            }
            return hash;
        }
    }
}
