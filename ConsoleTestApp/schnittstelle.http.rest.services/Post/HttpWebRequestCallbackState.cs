using System;
using System.IO;

namespace schnittstelle.http.rest.services.post
{
    /// <summary>
    /// This class is passed on to the user supplied callback method
    /// as a parameter. If there was an exception during the process
    /// then the Exception property will not be null and will hold
    /// a reference to the Exception that was raised.
    /// The ResponseStream property will be not null in the case of
    /// a sucessful request/response cycle. Use this stream to
    /// exctract the response.
    /// </summary>
    public class HttpWebRequestCallbackState
    {
        public Stream ResponseStream { get; private set; }
        public Exception Exception { get; private set; }
        public Object State { get; set; }

        public HttpWebRequestCallbackState(Stream responseStream, object state)
        {
            ResponseStream = responseStream;
            State = state;
        }

        public HttpWebRequestCallbackState(Exception exception)
        {
            Exception = exception;
        }
    }
}
