using PlexureAPITest.Constants;
using PlexureAPITest.Helpers;
using System.Net;

namespace PlexureAPITest.TestData
{
    public static class LoginTestData
    {
        public static object[] InvalidCredentialsValidation = new object[]
        {
            new object[] { "", TestConfig1.Password },
            new object[] { null, TestConfig1.Password },
            new object[] { TestConfig1.Username, ""},
            new object[] { TestConfig1.Username, null },
            new object[] { CharacterGenerator.GenerateRandomValue(Enums.CharacterGeneratorType.Ascii), CharacterGenerator.GenerateRandomValue(Enums.CharacterGeneratorType.Numbers)}
        };

        public static object[] MandatoryFieldsValidation = new object[]
        {
            new object[] { "",""},
            new object[] { null, null }
        };
    }
}