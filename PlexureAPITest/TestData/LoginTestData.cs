using PlexureAPITest.Helpers;

namespace PlexureAPITest.TestData
{
    public static class LoginTestData
    {
        public static object[] MandatoryFieldsValidation = new object[]
        {
            new object[] { "", TestConfig1.Password },
            new object[] { null, TestConfig1.Password },
            new object[] { TestConfig1.Username, "" },
            new object[] { TestConfig1.Username, null },
            new object[] { "","" },
            new object[] { null, null },
            new object[] { CharacterGenerator.GenerateRandomValue(Enums.CharacterGeneratorType.Numbers), CharacterGenerator.GenerateRandomValue(Enums.CharacterGeneratorType.Numbers)},
            new object[] { CharacterGenerator.GenerateRandomValue(Enums.CharacterGeneratorType.Unicode), CharacterGenerator.GenerateRandomValue(Enums.CharacterGeneratorType.Unicode) },
        };
    }
}