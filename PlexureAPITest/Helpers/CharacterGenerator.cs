using PlexureAPITest.Enums;
using System;
using System.Linq;

namespace PlexureAPITest.Helpers
{
    public static class CharacterGenerator
    {
        private static readonly Random Random = new Random();
        private const string Ascii = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const string Number = "0123456789";

        public static string GenerateRandomValue(CharacterGeneratorType characterGeneratorType, int stringLength = 10,
        bool whitespace = false)
        {
            string randomString;
            switch (characterGeneratorType)
            {
                case CharacterGeneratorType.Ascii:
                    randomString = RandomAsciiString(stringLength, whitespace);
                    break;
                case CharacterGeneratorType.Numbers:
                    randomString = RandomNumberString(stringLength, whitespace);
                    break;
                default:
                    throw new ArgumentException("Invalid string type specified.");
            }

            return randomString;
        }

        private static string RandomAsciiString(int length, bool whitespace = false)
        {
            return GenerateString(Ascii, length, whitespace);
        }

        private static string RandomNumberString(int length, bool whitespace = false)
        {
            string generatedNumber = GenerateString(Number, length, whitespace);
            return generatedNumber.StartsWith("0") ? RandomNumberString(length, whitespace) : generatedNumber;
        }

        private static string GenerateString(string option, int length, bool whitespace = false)
        {
            string generatedString = new string(Enumerable.Repeat(option, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
            if (whitespace)
            {
                int halfStringLength = generatedString.Length / 2;
                generatedString = generatedString.Insert(halfStringLength, " ");
            }

            return generatedString;
        }
    }
}