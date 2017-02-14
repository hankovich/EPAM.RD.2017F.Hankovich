using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Service.Generator;
using Service.Exceptions;

namespace Service.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        [Test]
        public void Add_WithIncompleteUser()
        {
            IdGenerator idGenerator = new IdGenerator();
            Service service = new Service(idGenerator);
            User user = new User
            {
                DateOfBirth = DateTime.Today,
                FirstName = "Michael"
            };
            Assert.Throws(typeof(IncompleteProfileException), () => service.Add(user));
        }

        [Test]
        public void Add_WithDuplicateUsers()
        {
            IdGenerator idGenerator = new IdGenerator();
            Service service = new Service(idGenerator);
            User user = new User
            {
                DateOfBirth = DateTime.Today,
                FirstName = "Michael",
                LastName = "Jackson"
            };
            service.Add(user);
            Assert.Throws(typeof(DuplicateUserException), () => service.Add(user));
        }

        [Test]
        public void Add_WithNullUser()
        {
            IdGenerator idGenerator = new IdGenerator();
            Service service = new Service(idGenerator);
            User user = null;
            Assert.Throws(typeof(ArgumentNullException), () => service.Add(user));
        }

        [Test]
        public void AddParams_DuplicateInParams()
        {
            IdGenerator idGenerator = new IdGenerator();
            Service service = new Service(idGenerator);
            User user = new User
            {
                DateOfBirth = DateTime.Today,
                FirstName = "Michael",
                LastName = "Jackson"
            };
            Assert.Throws(typeof(DuplicateUserException), () => service.Add(user, user));
        }

        [Test]
        public void AddParams_DuplicateInParamsAndRepo()
        {
            IdGenerator idGenerator = new IdGenerator();
            Service service = new Service(idGenerator);
            User user = new User
            {
                DateOfBirth = DateTime.Today,
                FirstName = "Michael",
                LastName = "Jackson"
            };

            User anotherUser = new User
            {
                DateOfBirth = DateTime.Today,
                FirstName = "John",
                LastName = "Lennon"
            };
            service.Add(user);
            Assert.Throws(typeof(DuplicateUserException), () => service.Add(user, anotherUser));
        }

        [Test]
        public void AddParams_IncompleteProfile()
        {
            IdGenerator idGenerator = new IdGenerator();
            Service service = new Service(idGenerator);
            User user = new User
            {
                DateOfBirth = DateTime.Today,
                FirstName = "Michael",
                LastName = "Jackson"
            };

            User anotherUser = new User
            {
                DateOfBirth = DateTime.Today,
                LastName = "Lennon"
            };
            service.Add(user);
            Assert.Throws(typeof(DuplicateUserException), () => service.Add(user, anotherUser));
        }
    }
}
