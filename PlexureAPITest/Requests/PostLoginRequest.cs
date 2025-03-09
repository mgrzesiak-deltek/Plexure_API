﻿using NUnit.Framework;
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
                Assert.That(actualUserDetails.Username, Is.EqualTo(TestConfig1.Username));
                Assert.That(actualUserDetails.AccessToken, Is.EqualTo("37cb9e58-99db-423c-9da5-42d5627614c5"));
            });
        }
    }
}