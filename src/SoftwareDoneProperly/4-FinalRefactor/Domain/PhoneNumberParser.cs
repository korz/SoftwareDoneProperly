namespace Domain
{
    public static class PhoneNumberParser
    {
        public static string Parse(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return null;
            }

            var phoneParts = phoneNumber.Split(' ');

            phoneNumber = phoneParts[0]
                .Replace(".", "-")
                .Replace("(", "")
                .Replace(")", "-");

            if (phoneNumber.StartsWith("+1-")) { phoneNumber = phoneNumber.Substring(3); }
            if (phoneNumber.StartsWith("+1 -")) { phoneNumber = phoneNumber.Substring(4); }
            if (phoneNumber.StartsWith("+1- ")) { phoneNumber = phoneNumber.Substring(4); }
            if (phoneNumber.StartsWith("+1- ")) { phoneNumber = phoneNumber.Substring(5); }
            if (phoneNumber.StartsWith("+1 - ")) { phoneNumber = phoneNumber.Substring(6); }
            if (phoneNumber.StartsWith("1-")) { phoneNumber = phoneNumber.Substring(2); }
            if (phoneNumber.StartsWith("1 -")) { phoneNumber = phoneNumber.Substring(3); }
            if (phoneNumber.StartsWith("1- ")) { phoneNumber = phoneNumber.Substring(3); }
            if (phoneNumber.StartsWith("1 - ")) { phoneNumber = phoneNumber.Substring(3); }

            return phoneNumber;
        }
    }
}
