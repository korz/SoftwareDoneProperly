using NUnit.Framework;

namespace Domain.Tests
{
    public class PhoneNumberParserTests
    {
        #region Happy Path
        [Test]
        public void Parse_Valid()
        {
            var phoneNumber = "+1-(313)123.4567";
            var expected = "313-123-4567";
            var parsedPhoneNumber = PhoneNumberParser.Parse(phoneNumber);

            Assert.AreEqual(expected, parsedPhoneNumber);
        }

        [Test]
        public void Parse_AlreadyParsed()
        {
            var phoneNumber = "313-123-4567";
            var expected = "313-123-4567";
            var parsedPhoneNumber = PhoneNumberParser.Parse(phoneNumber);

            Assert.AreEqual(expected, parsedPhoneNumber);
        }
        #endregion

        #region Un-Happy Path
        [Test]
        public void Parse_Invalid()
        {
            var phoneNumber = "12345";
            var expected = "12345";
            var parsedPhoneNumber = PhoneNumberParser.Parse(phoneNumber);

            Assert.AreEqual(expected, parsedPhoneNumber);
        }

        [Test]
        public void Parse_Blank()
        {
            var phoneNumber = "";
            var expected = "";
            var parsedPhoneNumber = PhoneNumberParser.Parse(phoneNumber);

            Assert.AreEqual(expected, parsedPhoneNumber);
        }

        [Test]
        public void Parse_Null()
        {
            string phoneNumber = null;
            string expected = null;
            var parsedPhoneNumber = PhoneNumberParser.Parse(phoneNumber);

            Assert.AreEqual(expected, parsedPhoneNumber);
        }
        #endregion
    }
}
