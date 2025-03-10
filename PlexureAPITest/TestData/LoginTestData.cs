using PlexureAPITest.Constants;
using PlexureAPITest.Helpers;
using System.Net;

namespace PlexureAPITest.TestData
{
    public static class LoginTestData
    {

        public static object[] InvalidCredentialsValidation = new object[]
        {
            new object[] { "", TestConfig.Password },
            new object[] { null, TestConfig.Password },
            new object[] { TestConfig.Username, ""},
            new object[] { TestConfig.Username, null },
            new object[] { "RandomNumber", "RandomAscii" }
        };

        public static object[] MandatoryFieldsValidation = new object[]
        {
            new object[] { "",""},
            new object[] { null, null }
        };
    }
}