using NUnit.Framework;
using PlexureAPITest.Loggers;
using PlexureAPITest.Model;

namespace PlexureAPITest.Requests
{
    public class PostLoginRequest : BaseRequest
    {
        public const string Address = "/login";

        public PostLoginRequest(Log log) : base(log) { }

        public dynamic CreateLoginRequest(string username, string password)
        {
            dynamic userRequest = new
            {
                Username = username,
                Password = password
            };

            Log.Info($"PostLogin request with '{userRequest.Username}' username has been created.");
            return userRequest;
        }

        public void CheckIfLoginBasicDetailsAreAsExpected(UserDto actualUserDetails, int expectedUserId)
        {
            Log.Info($"User with {actualUserDetails.Username} username has been logged successfully.");
            Assert.Multiple(() =>
            {
                Assert.That(actualUserDetails.UserId, Is.EqualTo(expectedUserId)); // currently passed from test but might be taken from database.
                Assert.That(actualUserDetails.Username, Is.EqualTo(TestConfig.Username));
                Assert.That(actualUserDetails.AccessToken, Is.EqualTo(TestConfig.DefaultToken));
            });
        }
    }
}