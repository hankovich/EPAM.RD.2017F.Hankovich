using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Exceptions;
using Service.Generator;

namespace Service
{
    public class Service
    {
        private readonly List<User> users;
        private readonly IIdGenerator idGenerator;

        public Service(IIdGenerator generator)
        {
            if (generator == null)
                throw new ArgumentNullException();
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
                throw new ArgumentNullException();
            if (user.LastName == null || user.FirstName == null)
            {
                throw new IncompleteProfileException();
            }

            user.Id = idGenerator.GenerateId(user);

            if (users.Exists(userInCollection => userInCollection.Id == user.Id))
                throw new DuplicateUserException();

            users.Add(user);
        }

        /// <summary>
        /// Add params array to repository
        /// </summary>
        /// <param name="lotsOfUsers">Params array to add</param>
        /// <exception cref="ArgumentNullException">Throws when <see cref="lotsOfUsers"/> is null</exception>
        /// <exception cref="IncompleteProfileException">Throws when at least one of the users' profiles in <see cref="lotsOfUsers"/> is incomplete</exception>
        /// <exception cref="DuplicateUserException">Throws when you're trying to add user which is already in repository or there are some equals users in <see cref="lotsOfUsers"/></exception>
        public void Add(params User[] lotsOfUsers)
        {
            if (lotsOfUsers == null)
                throw new ArgumentNullException();

            foreach (var user in lotsOfUsers)
            {
                if (user?.LastName == null || user.FirstName == null)
                {
                    throw new IncompleteProfileException();
                }

                if (users.Exists(userInCollection => userInCollection.Id == idGenerator.GenerateId(user))
                    || lotsOfUsers.Count(userInCollection => userInCollection.Id == idGenerator.GenerateId(user)) != 1)
                    throw new DuplicateUserException();
            }

            foreach (var user in lotsOfUsers)
            {
                Add(user);
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
                    return false;

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
        /// <exception cref="ArgumentNullException">Throws when <see cref="criterion"/> is null</exception>
        public void Delete(Predicate<User> criterion)
        {
            if (criterion == null)
                throw new ArgumentNullException();

            foreach (var user in users.Where(user => criterion(user)))
            {
                users.Remove(user);
            }
        }

        /// <summary>
        /// Delete user from repository
        /// </summary>
        /// <param name="user">User to delete</param>
        /// <returns>True if this user was deleted successfully, otherwise, false</returns>
        public bool TryDelete(User user)
        {
            if (user?.FirstName == null || user.LastName == null)
                return false;
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
        /// Delete params array from repository
        /// </summary>
        /// <param name="lotsOfUsers">Params array of users to delete</param>
        /// <returns>True if all users from array was successfully deleted, false, otherwise</returns>
        public bool TryDelete(params User[] lotsOfUsers)
        {
            if (lotsOfUsers == null)
                return false;

            bool result = true;
            foreach (var user in lotsOfUsers)
            {
                if (user?.FirstName == null || user.LastName == null)
                    result = false;
                else
                {
                    try
                    {
                        user.Id = idGenerator.GenerateId(user);
                        result = users.Remove(user) && result;
                    }
                    catch
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Search for users in repository via predicate
        /// </summary>
        /// <param name="criterion">Search criterion</param>
        /// <returns>Collection of users which fit the criterion</returns>
        /// <exception cref="ArgumentNullException">Throws when <see cref="criterion"/> is null</exception>
        public IEnumerable<User> Search(Predicate<User> criterion)
        {
            if (criterion == null)
                throw new ArgumentNullException(nameof(criterion));
            return users.Where(new Func<User, bool>(criterion));
        }
    }
}