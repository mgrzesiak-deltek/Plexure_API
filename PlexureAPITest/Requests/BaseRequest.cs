using PlexureAPITest.Loggers;

namespace PlexureAPITest.Requests
{
    public abstract class BaseRequest
    {
        protected readonly Log Log;

        public BaseRequest (Log log)
        {
            Log = log;
        }
    }
}