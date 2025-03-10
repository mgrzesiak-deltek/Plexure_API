using NUnit.Framework;
using PlexureAPITest.Components;
using PlexureAPITest.Constants;
using PlexureAPITest.Helpers;
using PlexureAPITest.Model;
using PlexureAPITest.Requests;
using RestSharp;
using System.Net;

namespace PlexureAPITest.Tests
{
    [TestFixture]
    public class PurchaseTests : EachTestRunner
    {
        private const int UserId = 1;

        private GetPoints _getPointsRequest;
        private PostPurchase _postPurchase;
        private ErrorMessageComponent _errorMessagesComponent;

        [SetUp]
        public void Setup()
        {
            _getPointsRequest = new GetPoints(Log);
            _postPurchase = new PostPurchase(Log);
            _errorMessagesComponent = new ErrorMessageComponent(Log);
        }

        [Test]
        [Category("validation")]
        [Description("Check: Status Code; Response details.")]
        public void Points_GetPoints_01()
        {
            // NOTES: 
            // There is a typo in spec since GET request should not have body
            // I'm wondering why we're returning 202 instead of 200
            
            RestResponse restResponse = ApiHelper.PrepareRestResponseForGetRequest(GetPoints.Address);
            PointDto getPointsResponseDetails = ApiHelper.DeserializeRestResponseToDynamicObject(restResponse).ToObject<PointDto>();
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(restResponse, HttpStatusCode.Accepted);
            _getPointsRequest.CheckIfGetPointsBasicDetailsAreAsExpected(getPointsResponseDetails, UserId);
        }

        [Test]
        [Category("validation")]
        [Description("Check: Status Code; Response details; Points have been updated.")]
        public void Purchase_PostPurchase_01()
        {
            int productId = 0; // do not know what is the valid product id

            // get current points for given user id
            RestResponse restResponse = ApiHelper.PrepareRestResponseForGetRequest(GetPoints.Address);
            PointDto getPointsResponseDetails = ApiHelper.DeserializeRestResponseToDynamicObject(restResponse).ToObject<PointDto>();
            int currentPointsForGivenUserId = getPointsResponseDetails.Points;
            // purchase a product
            RestResponse purchaseResponse = ApiHelper.PrepareRestResponseForPostRequest(PostPurchase.Address, _postPurchase.CreatePurchaseRequest(productId));
            PurchaseDto purchaseResponseDetails = ApiHelper.DeserializeRestResponseToDynamicObject(purchaseResponse).ToObject<PurchaseDto>();
            RestResponse secondGetPointsResponse = ApiHelper.PrepareRestResponseForGetRequest(GetPoints.Address);
            PointDto secondGetPointsResponseDetails = ApiHelper.DeserializeRestResponseToDynamicObject(restResponse).ToObject<PointDto>();
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(restResponse, HttpStatusCode.Accepted);
            _postPurchase.CheckIfPurchaseBasicDetailsAreAsExpected(purchaseResponseDetails);// to be done since I do not know how the response looks like
            Assert.That(currentPointsForGivenUserId + 100, Is.EqualTo(secondGetPointsResponseDetails.Points));
        }

        [Test]
        [Category("validation")]
        [Description("Check: Try to do purchase with not existing product id.")]
        public void Purchase_PostPurchaseNegativeScenario_01()
        {
            int productId = int.MaxValue;
            RestResponse purchaseResponse = ApiHelper.PrepareRestResponseForPostRequest(PostPurchase.Address, _postPurchase.CreatePurchaseRequest(productId));
            //validations
            ApiHelper.CheckIfStatusCodeIsAsExpected(purchaseResponse, HttpStatusCode.BadRequest);
            _errorMessagesComponent.CheckErrorMessage(purchaseResponse.Content, ErrorMessages.InvalidProductIdErrorMessage);
        }
    }
}