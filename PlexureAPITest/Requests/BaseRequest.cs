using PlexureAPITest.Loggers;
using System;

namespace PlexureAPITest.Requests
{
    public abstract class BaseRequest
    {
        protected readonly Log Log;
        protected readonly Random Random = new Random();

        public BaseRequest (Log log)
        {
            Log = log;
        }
    }
}