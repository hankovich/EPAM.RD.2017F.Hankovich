using System;

namespace Service
{
    /// <summary>
    /// User of the <see cref="Service"/>
    /// </summary>
    public class User : IEquatable<User>
    {
        /// <summary>
        /// id of user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth of user
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Check the equality with other user
        /// </summary>
        /// <param name="other">User to check the equality with</param>
        /// <returns>True if users are equal and false otherwise</returns>
        public bool Equals(User other)
        {
            return Id == other?.Id;
        }
    }
}
