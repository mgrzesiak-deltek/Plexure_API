using NUnit.Framework;
using System.Net;

namespace PlexureAPITest
{
    [TestFixture]
    public class Test
    {
        Service service;

        [OneTimeSetUp]
        public void Setup()
        {
            service = new Service();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (service != null)
            {
                service.Dispose();
                service = null;
            }
        }


        //Example test cases:
        //Candidate can add as many test cases as you can. 
        //You can also add some comments if need. 

        [Test]
        public void TEST_001_Login_With_Valid_User()
        {
            var response = service.Login("Tester", "Plexure123");

            response.Expect(HttpStatusCode.OK);
            //NOTE: Please rerun this test to wakeup service if you got 500 server errors
        }

        [Test]
        public void TEST_002_Get_Points_For_Logged_In_User()
        {
            var response = service.GetPoints();
            response.Expect(HttpStatusCode.Accepted); // 202

            var points = response.Entity;
            Assert.AreEqual(1, points.UserId);

            //NOTE: The points might get updated by different purchases on the same user,
            //      so we only test if the points > zero in this test.
            Assert.Greater(points.Value, 0);
        }
    }
        
}
