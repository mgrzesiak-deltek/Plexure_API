using NUnit.Framework;
using PlexureAPITest.Loggers;
using PlexureAPITest.Model;

namespace PlexureAPITest.Requests
{
    public class PostPurchase : BaseRequest
    {
        public const string Address = "/purchase";

        public PostPurchase(Log log) : base(log) { }

        public dynamic CreatePurchaseRequest(int productId)
        {
            dynamic purchaseRequest = new
            {
                product_id = productId
            };

            Log.Info($"PostPurchase request with '{purchaseRequest.product_id}' product id has been created.");
            return purchaseRequest;
        }

        public void CheckIfPurchaseBasicDetailsAreAsExpected(PurchaseDto actualPurchaseDetails)
        {
            Log.Info($"Purchase with {actualPurchaseDetails.ProductId} product id has been completed.");
            Assert.Multiple(() =>
            {
                Assert.That(actualPurchaseDetails.ProductId, Is.EqualTo(1));
            });
        }
    }
}