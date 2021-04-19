using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace schnittstelle.http.rest.services.post
{
    // http://www.matlus.com/httpwebrequest-asynchronous-programming/#codelisting2

    public static class HttpSocket
    {
        static HttpWebRequest CreateHttpWebRequest(string url, string httpMethod, string contentType)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = httpMethod;
            return httpWebRequest;
        }

        static byte[] GetRequestBytes(NameValueCollection postParameters)
        {
            if (postParameters == null || postParameters.Count == 0)
                return new byte[0];
            var sb = new StringBuilder();
            foreach (var key in postParameters.AllKeys)
                sb.Append(key + "=" + postParameters[key] + "&");
            sb.Length = sb.Length - 1;
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        static void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
        {
            Stream requestStream = null;
            HttpWebRequestAsyncState asyncState = null;
            try
            {
                asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
                string postData = File.ReadAllText(@"C:\temp\json.txt");
                asyncState.RequestBytes = Encoding.UTF8.GetBytes(postData);

                requestStream = asyncState.HttpWebRequest.EndGetRequestStream(asyncResult);
                requestStream.Write(asyncState.RequestBytes, 0, asyncState.RequestBytes.Length);
                requestStream.Close();

                asyncState.HttpWebRequest.BeginGetResponse(BeginGetResponseCallback,
                  new HttpWebRequestAsyncState
                  {
                      HttpWebRequest = asyncState.HttpWebRequest,
                      ResponseCallback = asyncState.ResponseCallback,
                      State = asyncState.State
                  });
            }
            catch (Exception ex)
            {
                if (asyncState != null)
                    asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
                else
                    throw;
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
            }
        }

        static void BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            WebResponse webResponse = null;
            Stream responseStream = null;
            HttpWebRequestAsyncState asyncState = null;
            try
            {
                asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
                webResponse = asyncState.HttpWebRequest.EndGetResponse(asyncResult);
                responseStream = webResponse.GetResponseStream();
                var webRequestCallbackState = new HttpWebRequestCallbackState(responseStream, asyncState.State);
                asyncState.ResponseCallback(webRequestCallbackState);
                responseStream.Close();
                responseStream = null;
                webResponse.Close();
                webResponse = null;
            }
            catch (Exception ex)
            {
                if (asyncState != null)
                    asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
                else
                    throw;
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
                if (webResponse != null)
                    webResponse.Close();
            }
        }

        /// <summary>
        /// If the response from a remote server is in text form
        /// you can use this method to get the text from the ResponseStream
        /// This method Disposes the stream before it returns
        /// </summary>
        /// <param name="responseStream">The responseStream that was provided in the callback delegate's HttpWebRequestCallbackState parameter</param>
        /// <returns></returns>
        public static string GetResponseText(Stream responseStream)
        {
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// This method does an Http POST sending any post parameters to the url provided
        /// </summary>
        /// <param name="url">The url to make an Http POST to</param>
        /// <param name="postParameters">The form parameters if any that need to be POSTed</param>
        /// <param name="responseCallback">The callback delegate that should be called when the response returns from the remote server</param>
        /// <param name="state">Any state information you need to pass along to be available in the callback method when it is called</param>
        /// <param name="contentType">The Content-Type of the Http request</param>
        /// 
        public static void PostAsync(string url, 
          Action<HttpWebRequestCallbackState> responseCallback,
          NameValueCollection postParameters,
          byte[] postBytes = null,
          object state = null,
          string contentType = "application/json; charset=utf-8")
        {
            var httpWebRequest = CreateHttpWebRequest(url, "POST", contentType);

            var requestBytes = GetRequestBytes(postParameters);
            if (postBytes != null)
                httpWebRequest.ContentLength = postBytes.Length;
            else httpWebRequest.ContentLength = requestBytes.Length;

            httpWebRequest.BeginGetRequestStream(BeginGetRequestStreamCallback,
                                                  new HttpWebRequestAsyncState()
                                                  {
                                                      RequestBytes = requestBytes,
                                                      HttpWebRequest = httpWebRequest,
                                                      ResponseCallback = responseCallback,
                                                      State = state
                                                  });
        }
    }
}
