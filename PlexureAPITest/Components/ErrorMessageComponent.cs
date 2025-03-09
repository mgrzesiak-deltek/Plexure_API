using NUnit.Framework;
using PlexureAPITest.Loggers;
using PlexureAPITest.Requests;

namespace PlexureAPITest.Components
{
    public class ErrorMessageComponent : BaseRequest
    {
        public ErrorMessageComponent(Log log) : base(log) { }

        public void CheckErrorMessage(string actualErrorMessage, string expectedErrorMessage)
        {
            string normalizedActualErrorMessage = actualErrorMessage.Contains("\"") ? actualErrorMessage.Replace("\"", "") : actualErrorMessage;
            string trimedActualErrorMessage = normalizedActualErrorMessage.Substring(6).Trim();
            if (!Equals(trimedActualErrorMessage, expectedErrorMessage))
            {
                Log.Info($"Actual error message in response: '{trimedActualErrorMessage}' is not as expected: '{expectedErrorMessage}'");
                Assert.Fail();
            }
        }
    }
}