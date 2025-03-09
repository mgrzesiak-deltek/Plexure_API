using NUnit.Framework;
using PlexureAPITest.Loggers;
using PlexureAPITest.Model;

namespace PlexureAPITest.Requests
{
    public class GetPoints : BaseRequest
    {
        public const string Address = "/points";

        public GetPoints(Log log) : base(log) { }

        public void CheckIfGetPointsBasicDetailsAreAsExpected(PointDto actualPointsDetails, int expectedUserId)
        {
            Log.Info($"User with {actualPointsDetails.UserId} UserId has {actualPointsDetails.Points} points.");
            Assert.Multiple(() =>
            {
                Assert.That(actualPointsDetails.UserId, Is.EqualTo(expectedUserId)); // currently passed from test but might be taken from database.
                Assert.That(actualPointsDetails.Points, Is.GreaterThan(0));
            });
        }
    }
}