using Contracts.Interfaces;
using Contracts.Models;
using NUnit.Framework;

namespace Domain.Tests
{
    class CustomerParserTests
    {
        private ICustomerParser customerParser { get; set; }

        [SetUp]
        public void Setup()
        {
            customerParser = new CustomerParser();
        }

        [Test]
        public void Parse_Valid_KnownTitle()
        {
            var phoneNumber = "+1-(313)123.4567";
            var expectedPhoneNumber = "313-123-4567";
            var customer = new Customer { CellPhone = phoneNumber, Title = "Programmer" };

            var parsedCustomer = customerParser.Parse(customer);

            Assert.AreEqual(expectedPhoneNumber, parsedCustomer.CellPhone);
            Assert.AreEqual("PROGRAMMER", parsedCustomer.Title);
        }

        [Test]
        public void Parse_Valid_UnknownTitle()
        {
            var phoneNumber = "+1-(313)123.4567";
            var expectedPhoneNumber = "313-123-4567";
            var customer = new Customer { CellPhone = phoneNumber, Title = "Unknown" };

            var parsedCustomer = customerParser.Parse(customer);

            Assert.AreEqual(expectedPhoneNumber, parsedCustomer.CellPhone);
            Assert.AreEqual("SCRUM MASTER", parsedCustomer.Title);
        }
    }
}
