using NUnit.Framework;
using API.Common.Services;
using API.Common.Models;

namespace APITest
{
    public class SetPhoneNumberTest
    {
        [Test]
        public void EmptyPhoneNumberTest()
        {
            Customer c = new Customer()
            {
                Id = 1,
                PhoneNumber = null,
                FirstName = "Peter",
                LastName = "Green",
                State = "NSW",
                Age = 56,
            };
            string phoneNumber = APIServices.SetPhoneNumber(c);
            Assert.AreEqual(phoneNumber, "Missing or invalid");
        }

        [Test]
       public void PhoneNumberLengthTest()
        {
            Customer c = new Customer()
            {
                Id = 1,
                PhoneNumber = "12345678923",  
                FirstName = "Peter",
                LastName = "Green",
                State = "NSW",
                Age = 56,
            };
            string phoneNumber = APIServices.SetPhoneNumber(c);
            Assert.AreEqual(phoneNumber, "Missing or invalid");
        }

        [Test]
        public void RemoveNonDigitalTest()
        {
            Customer c = new Customer()
            {
                Id = 1, 
                PhoneNumber = "12345-6789",
                FirstName = "Peter",
                LastName = "Green",
                State = "NSW",
                Age = 56,
            };
            string phoneNumber = APIServices.SetPhoneNumber(c);
            Assert.AreEqual(phoneNumber, "(02) 123456789");
        }

        [Test]
        public void DeriveStateCodeTest()
        {
            Customer c = new Customer()
            {
                Id = 1, 
                PhoneNumber = "12345-6789",
                FirstName = "Peter",
                LastName = "Green",
                State = "QLD",
                Age = 56,
            };
            string phoneNumber = APIServices.SetPhoneNumber(c);
            Assert.AreEqual(phoneNumber, "(07) 123456789");
        }
    }
}