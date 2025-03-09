using NUnit.Framework;
using PlexureAPITest.Helpers;
using PlexureAPITest.Model;
using PlexureAPITest.Requests;
using RestSharp;
using System.Net;

namespace PlexureAPITest.Tests
{
    [TestFixture]
    public class LoginTests : EachTestRunner
    {
        private PostLoginRequest _postLoginRequest;

        [SetUp]
        public void Setup()
        {
            _postLoginRequest = new PostLoginRequest(Log);
        }
        
        [Test]
        [Category("validation")]
        [Description("Check: Status Code; Response details.")]
        public void WhenLoginWithValidUserThenLoggedSuccessfully()
        {
            var expectedUserId = 1;
            dynamic loginRequestBody = _postLoginRequest.CreateLoginRequest(TestConfig1.Username, TestConfig1.Password);
            RestResponse restResponse = ApiHelper.PrepareRestResponseForPostRequest(
                PostLoginRequest.Address,
                loginRequestBody);
            UserDto loginResponseDetails = ApiHelper.DeserializeRestResponseToDynamicObject(restResponse).ToObject<UserDto>();
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(restResponse, HttpStatusCode.OK);
            _postLoginRequest.CheckIfLoginBasicDetailsAreAsExpected(loginResponseDetails, expectedUserId);
        }
    }
}