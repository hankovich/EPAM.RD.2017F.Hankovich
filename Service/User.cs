using System;

namespace Service
{
    public class User: IEquatable<User>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool Equals(User other)
        {
            return Id == other?.Id;
        }
    }
}
