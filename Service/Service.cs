using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Exceptions;
using Service.Generator;

namespace Service
{
    /// <summary>
    /// Service that provide API to work with group of <see cref="User"/>s
    /// </summary>
    public class Service
    {
        private readonly List<User> users;
        private readonly IIdGenerator idGenerator;

        /// <summary>
        /// Create new instance of Service
        /// </summary>
        /// <param name="generator"></param>
        public Service(IIdGenerator generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException();
            }

            idGenerator = generator;
            users = new List<User>();
        }

        /// <summary>
        /// Adding a single user to repository
        /// </summary>
        /// <param name="user">User to add</param>
        /// <exception cref="ArgumentNullException">Throws when you try to add null user to repository</exception>
        /// <exception cref="IncompleteProfileException">Throws when profile of user you're trying to add is incomplite (some fields are missed)</exception>
        /// <exception cref="DuplicateUserException">Throws when you're trying to add user which already exists</exception>
        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            if (user.LastName == null || user.FirstName == null)
            {
                throw new IncompleteProfileException();
            }

            user.Id = idGenerator.GenerateId(user);

            if (users.Exists(userInCollection => userInCollection.Id == user.Id))
            {
                throw new DuplicateUserException();
            }

            users.Add(user);
        }

        /// <summary>
        /// Adding collection of users to repository
        /// </summary>
        /// <param name="usersCollection">Collection to add</param>
        /// <exception cref="ArgumentNullException">Throws when at least one of the users in the collection is null or when the whole collection is null</exception>
        /// <exception cref="IncompleteProfileException">Throws when at least one of the users in the collection is incomplite (some fields are missed)</exception>
        /// <exception cref="DuplicateUserException">Throws when you're trying to add user which already exists or you have duplicate users in the collection</exception>
        public void Add(IEnumerable<User> usersCollection)
        {
            if (usersCollection == null)
            {
                throw new ArgumentNullException();
            }

            IEnumerable<User> usersInList = usersCollection as IList<User> ?? usersCollection.ToList();

            foreach (var user in usersInList)
            {
                Add(user);
            }
        }

        /// <summary>
        /// Trying to add a single user to repository
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>True if user is valid and was succesfully added to repository, false otherwise</returns>
        public bool TryAdd(User user)
        {
            try
            {
                if (user?.LastName == null || user.FirstName == null)
                {
                    return false;
                }

                user.Id = idGenerator.GenerateId(user);

                if (users.Exists(userInCollection => userInCollection.Id == user.Id))
                {
                    return false;
                }

                users.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Try to add collection of users to repository
        /// </summary>
        /// <param name="usersCollection">Collection to add</param>
        /// <returns>True if all users in the collection is valid and the collection is succesfully added to repository, false otherwise</returns>
        public bool TryAdd(IEnumerable<User> usersCollection)
        {
            try
            {
                if (usersCollection == null)
                {
                    return false;
                }

                bool isCorrect = true;

                IEnumerable<User> usersInList = usersCollection as IList<User> ?? usersCollection.ToList();

                foreach (var user in usersInList)
                {
                    if (user?.LastName == null
                        || user.FirstName == null
                        || users.Exists(userInCollection => userInCollection.Id == idGenerator.GenerateId(user))
                        || usersInList.Count(userInList => userInList.Id == idGenerator.GenerateId(user)) != 1)
                    {
                        isCorrect = false;
                    }
                }

                if (!isCorrect)
                {
                    return false;
                }

                foreach (var user in usersInList)
                {
                    Add(user);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete user from repository via predicate
        /// </summary>
        /// <param name="criterion">Criterion to delete</param>
        /// <exception cref="ArgumentNullException">Throws when criterion is null</exception>
        public void Delete(Predicate<User> criterion)
        {
            if (criterion == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var user in users.Where(user => criterion(user)))
            {
                users.Remove(user);
            }
        }

        /// <summary>
        /// Trying to delete user from repository
        /// </summary>
        /// <param name="user">User to delete</param>
        /// <returns>True if this user was deleted successfully, otherwise, false</returns>
        public bool TryDelete(User user)
        {
            if (user?.FirstName == null || user.LastName == null)
            {
                return false;
            }

            try
            {
                user.Id = idGenerator.GenerateId(user);
                return users.Remove(user);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Search for users in repository via predicate
        /// </summary>
        /// <param name="criterion">Search criterion</param>
        /// <returns>Collection of users which fit the criterion</returns>
        /// <exception cref="ArgumentNullException">Throws when criterion is null</exception>
        public IEnumerable<User> Search(Predicate<User> criterion)
        {
            if (criterion == null)
            {
                throw new ArgumentNullException(nameof(criterion));
            }

            return users.Where(new Func<User, bool>(criterion));
        }
    }
}