using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace schnittstelle.http.rest.services
{
    public class CBwtHttpWebRequestHandler : IRequestHandler
    {
        public CBwtHttpWebRequestHandler(string _url)
        {
            this.HttpUrl = _url;
        }

        public string HttpUrl { get; set; }
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        #region
        public Dictionary<int, string> HttpValidationStatusMsgDictionary { get; set; } = new Dictionary<int, string>();
        public string HttpResponseString { get; set; }
        #endregion

        public void PostDataToValidate(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.KeepAlive = false;

            // <BeginGetRequestStream>: Begins an asynchronous request 
            //                          for a Stream object to use to write data.
            // param<AsyncCallback>: delegate AsyncCallback references a method to be called when a 
            //                          corresponding asynchronous operation completes.
            request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);

            Console.WriteLine(".....connecting");
            // This method blocks the current thread and wait for the signal by other thread.
            allDone.WaitOne();
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            
            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);

            string postData = File.ReadAllText(@"C:\temp\json.txt");
            // Convert the string into a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Write to the request stream.
            postStream.Write(byteArray, 0, postData.Length);
            postStream.Close();

            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            
            // End the operation
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();
            Console.WriteLine(responseString);
            // Close the stream object
            streamResponse.Close();
            streamRead.Close();

            // Release the HttpWebResponse
            response.Close();
            allDone.Set();
        }

        // Versuch sich mit dem Webdienst zu verbinden
        public bool CheckUrlConnection()
        {
            bool isValid = true;
            HttpWebResponse response = null;
            HttpStatusCode statusCode;
            Task.Run(() =>
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                                                    .Create(HttpUrl);
                    webRequest.AllowAutoRedirect = false;
                    webRequest.Timeout = 9000;
                    try
                    {
                        Console.WriteLine(".... verbinden");
                        response = (HttpWebResponse)webRequest.GetResponse();
                        isValid = true;
                    }
                    catch(WebException we)
                    {
                        response = (HttpWebResponse)we.Response;
                        isValid = false;
                    }
                    finally
                    {
                        statusCode = response.StatusCode;
                        HttpValidationStatusMsgDictionary.Add((int)statusCode, response.StatusDescription);
                    }
                }).Wait();
                
            return isValid;
        }
        /**** ************   **         *************************************************/
    }
}
