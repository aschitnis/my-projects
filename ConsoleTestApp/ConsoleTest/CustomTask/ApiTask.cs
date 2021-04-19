using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.CustomTask
{
    /**
     * Class ApiTask has different methods which shows different ways to run a task asnychronously.
     */
    public class ApiTask
    {
        // simulate a http rest call to webservice. The method itself is synchronous.
        private Exception WebApiCallGetNewsFeed(string mynews, out string responsenewsfeed)
        {
            long num1 = 999999919;
            for (int a = 2; a <= num1 / 2; a++)
            {
                //if (num1 % a == 0)
                //{
                //    // responsenewsfeed = Convert.ToString(num1);
                //    // Console.WriteLine(num1 + " is not prime number");
                //}
            }
            responsenewsfeed = mynews + "  Message number: " + Convert.ToString(num1);
            return null;
        }

        private Exception WebApiCallGetStockFeed(string myStock, out string responseStockfeed)
        {
            long num1 = 199999919;
            for (int a = 2; a <= num1 / 2; a++)
            {
                //if (num1 % a == 0)
                //{
                //    // responsenewsfeed = Convert.ToString(num1);
                //    // Console.WriteLine(num1 + " is not prime number");
                //}
            }
            responseStockfeed = myStock + "  Stock-Rate Message number: " + Convert.ToString(num1);
            return null;
        }

        // make multiple(optional) calls to webservice. 
        // We simulate a exception here on making a 4th call to the webservice.
        public Exception GetNewsFeed(int numberOfWebServiceCalls)
        {
            Exception ex = null;
            try
            {
                string responsenewsfeed = null;
                for (int i = 1; i < numberOfWebServiceCalls; i++)
                {
                    string mynews = $"Webservice call: {i} - Latest flash - {DateTime.Now.ToString()}";
                    WebApiCallGetNewsFeed(mynews, out responsenewsfeed);
                    if (i == 4)
                        return new Exception("4th call has a error. Execution cannot proceed for the remaining calls...");

                    Console.WriteLine($"{responsenewsfeed}");
                }
                return ex;
            }
            catch(Exception e)
            {
                return e;
            }
        }
        public Exception GetStocksFeed(int numberOfWebServiceCalls)
        {
            Exception ex = null;
            try
            {
                string responseStocknewsfeed = null;
                for (int i = 1; i < numberOfWebServiceCalls; i++)
                {
                    string myStockNews = $"Webservice call: {i} - Latest Stock High/Low  - {DateTime.Now.ToString()}";
                    WebApiCallGetStockFeed(myStockNews, out responseStocknewsfeed);
                    if (i == 8)
                        return new Exception("8th Stockfeed has a error. Execution cannot proceed for the remaining Stock feeds...");

                    Console.WriteLine($"{responseStocknewsfeed}");
                }
                return ex;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public Task<Exception> GetNewsFeedsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Exception ex = null;
                string responsenewsfeed = null;
                for (int i = 1; i < 3; i++)
                {
                    string mynews = "Latest flash ";
                    ex = WebApiCallGetNewsFeed(mynews, out responsenewsfeed);
                }
                return ex;
            });
        }

        public async Task<Exception> GetNewsFeedsTask()
        {
            return await GetNewsFeedsAsync();
        }
    }

    public class ApiManager
    {
        public ApiManager()    { Api = new ApiTask();   }

        public ApiTask Api { get; set; }
        public void CallWebserviceAsync()
        {
            var tasks = new List<Task>();
            Task t1 = Task.Factory.StartNew(() =>
            {
                Exception ex = Api.GetNewsFeed(5);
                if (ex != null)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            });
            Task t2 = Task.Factory.StartNew(() =>
            {
                Exception ex = Api.GetStocksFeed(10);
                if (ex != null)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            });
            tasks.Add(t1);
            tasks.Add(t2);
            Task.WaitAll(tasks.ToArray());
        }
    }
}
