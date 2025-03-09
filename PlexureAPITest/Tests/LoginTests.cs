using NUnit.Framework;
using PlexureAPITest.Components;
using PlexureAPITest.Constants;
using PlexureAPITest.Helpers;
using PlexureAPITest.Model;
using PlexureAPITest.Requests;
using PlexureAPITest.TestData;
using RestSharp;
using System.Net;

namespace PlexureAPITest.Tests
{
    [TestFixture]
    public class LoginTests : EachTestRunner
    {
        private PostLoginRequest _postLoginRequest;
        private ErrorMessageComponent _errorMessagesComponent;

        [SetUp]
        public void Setup()
        {
            _postLoginRequest = new PostLoginRequest(Log);
            _errorMessagesComponent = new ErrorMessageComponent(Log);
        }

        [Test]
        [Category("validation")]
        [Description("Check: Status Code; Response details.")]
        public void Login_PostLogin01()
        {
            // NOTES: 
            // IMHO this request should return only HttpStatusCode = OK without additional data, auth token should be returned by some other request which is done on the fly
            // I'm wondering if there is any mechanism which will block login after some number of tries.
            // IMHO there is no sense (unless there are some requirements) to distinguish error messages and http codes. Probably we can use one response where
            // HttpStatusCode = BadRequest and ErrorMessage = 'Username or password is incorrect'

            var expectedUserId = 1;
            dynamic loginRequestBody = _postLoginRequest.CreateLoginRequest(TestConfig.Username, TestConfig.Password);
            RestResponse restResponse = ApiHelper.PrepareRestResponseForPostRequest(
                PostLoginRequest.Address,
                loginRequestBody);
            UserDto loginResponseDetails = ApiHelper.DeserializeRestResponseToDynamicObject(restResponse).ToObject<UserDto>();
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(restResponse, HttpStatusCode.OK);
            _postLoginRequest.CheckIfLoginBasicDetailsAreAsExpected(loginResponseDetails, expectedUserId);
        }

        [Test, TestCaseSource(typeof(LoginTestData), nameof(LoginTestData.InvalidCredentialsValidation))]
        [Category("validation")]
        [Description("Check: Login negative scenarios - incorrect credentials")]
        public void Login_PostLoginNegativeScenario01(string username, string password)
        {
            dynamic loginRequestBody = _postLoginRequest.CreateLoginRequest(username, password);
            RestResponse restResponse = ApiHelper.PrepareRestResponseForPostRequest(
                PostLoginRequest.Address,
                loginRequestBody);
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(restResponse, HttpStatusCode.Unauthorized);
            _errorMessagesComponent.CheckErrorMessage(restResponse.Content, ErrorMessages.MismatchCredentialsErrorMessage);
        }

        [Test, TestCaseSource(typeof(LoginTestData), nameof(LoginTestData.MandatoryFieldsValidation))]
        [Category("validation")]
        [Description("Check: Login negative scenarios - missing mandatory field")]
        public void Login_PostLoginNegativeScenario02(string username, string password)
        {
            // NOTES: 
            // these tests are failing due to incorrect error message - in spec there is 'Username and password are required' but in response
            // I'm getting 'Username and password required.' - first BUG
            // according to the spec I should get HttpStatusCode.BadRequest when either username or password are blank/null but it works only when both fields are missing
            // - second BUG

            dynamic loginRequestBody = _postLoginRequest.CreateLoginRequest(username, password);
            RestResponse restResponse = ApiHelper.PrepareRestResponseForPostRequest(
                PostLoginRequest.Address,
                loginRequestBody);
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(restResponse, HttpStatusCode.BadRequest);
            _errorMessagesComponent.CheckErrorMessage(restResponse.Content, ErrorMessages.MissingCredentialsErrorMessage);
        }
    }
}