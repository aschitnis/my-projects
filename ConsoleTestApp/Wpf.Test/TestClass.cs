using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Wpf.Test
{
    public class TestClassOneVM : ViewModelBase
    {
        public event EventHandler<CancelEventArgs> CancelTask;
        public event EventHandler<CancelPrimeNumberCalculationEventArgs> CancelPrimeNumberCalculationTask;

        public CancellationTokenSource TokenSource = new CancellationTokenSource();
        public CancellationToken Token;

        private static long NUMBER = 2000000; 
        private string progressmessage;
        private string concatenatedvalue;
        private string resultPrimeNumber;

        // If the calculation of a prime number crosses the maxValue, then stop the calculation immediatedly.
        private long maxValue { get; set; }
        private Dictionary<long, long> primelist { get; set; } = new Dictionary<long, long>();
        private CancelProcessSubscriber subscriber { get; set; }
        private int Count { get; set; } = 0;
        public CEnums.Colours SelectedColor
        {
            get;set;
        }
        public string ResultPrimeNumber
        {
            get { return resultPrimeNumber; }
            set { resultPrimeNumber = value; OnChanged(); }
        }
        public long MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; OnChanged(); }
        }

        public List<CEnums.Colours> ColorList
        {
            get { return Enum.GetValues(typeof(CEnums.Colours)).OfType<CEnums.Colours>().ToList(); }
        }
        public string ConcatenatedValue
        {
            get { return concatenatedvalue; }
            set { concatenatedvalue = value; }
        }
        public string ProgressMessage
        {
            get { return progressmessage; }
            set
            {
                progressmessage = value;
                OnChanged();
            }
        }

        public TestClassOneVM()
        {
            subscriber = new CancelProcessSubscriber();
            CancelTask += subscriber.CancelProcessMethod;
            CancelPrimeNumberCalculationTask += subscriber.CancelPrimeNumberCalculation;
            Token = TokenSource.Token;
            MaxValue = 999999;
        }

        #region Calls to EventHandler 
        public void OnCancelPrimeCalculationTask()
        {
            if (CancelPrimeNumberCalculationTask != null)
            {
                CancelPrimeNumberCalculationTask(this, new CancelPrimeNumberCalculationEventArgs(ResultPrimeNumber.ToString()));
                TokenSource.Cancel();
            }
        }
        public void OnCancelCalculationTask()
        {
            if(CancelTask != null)
            {
                CancelTask(this, new CancelEventArgs(this.Count));
            }
        }
        #endregion

        public void GetCalculatedCountAsync()
        {
            Task<string> t = Task.Factory.StartNew<string>(() => { return GetLoopCount('X'); });
            ProgressMessage = "....calculation in progress";
            t.Wait();
            ConcatenatedValue = t.Result;
        }

        public void CalculateActionAsync()
        {
            Task t = Task.Factory.StartNew(() => { ConcatenateChars('X',200000); } );
            ProgressMessage = "....calculation in progress";
            t.Wait();
            ProgressMessage = "....calculation completed";
        }

        private void ConcatenateChars(char c, int count)
        {
            string concatenateString = String.Empty;
            for(int i=0;i<count;i++)
            {
               concatenateString += c;
            }
        }

        private string GetLoopCount(char c)
        {
            string concatenateString = String.Empty;
            int resultCount = 1;
            for (int i = 0; i < 200000; i++)
            {
                concatenateString += c;
                resultCount++;
            }
            return resultCount.ToString();
        }

        public void SetLoopCountUsingAsyncMethod()
        {
            Task<string> t = GetAsyncLoopCount('A');
            ConcatenatedValue = t.Result;
        }
        public void FindPrimeNumber()
        {
            Task<string> t = GetAsyncCalculatePrimeNumber();
            ResultPrimeNumber = t.Result;
        }

        public void TestLookUp()
        {
            List<string> countries = new List<string>() { "italy", "austria","france","germany","austria","india"};
            string name = countries.Where(x => x == "austi").DefaultIfEmpty("Hindostan").First();

            ResultPrimeNumber = name;
        }
        private async Task<string> GetAsyncCalculatePrimeNumber()
        {
            ProgressMessage = "....starting prime number calculation";
            Task<string> t = Task.Factory.StartNew<string>(() =>
                               {
                                   var token = Token;

                                   int count = 0;
                                   long a = 2;
                                   while (count < NUMBER)
                                   {
                                       long b = 2;
                                       int prime = 1;// to check if found a prime
                                       while (b * b <= a)
                                       {
                                           if (a % b == 0)
                                           {
                                               prime = 0;
                                               break;
                                           }
                                           b++;
                                       }
                                       if (prime > 0)
                                       {
                                           if (count >= 24) // MaxValue
                                           {
                                               ResultPrimeNumber = a.ToString();
                                               OnCancelPrimeCalculationTask();
                                           }

                                           if (Token.IsCancellationRequested)
                                           {
                                               // clean-up work before cancellation
                                               ProgressMessage = subscriber.SubscriberMessage;
                                               break; 
                                               /** this will throw a Exception of Task CancellationRequested
                                               Token.ThrowIfCancellationRequested();
                                               **/
                                           }

                                           count++;
                                           primelist.Add(count, a);
                                       }

                                       a++;

                                   }
                                   return (--a).ToString();
                               },Token);
            ProgressMessage = "....prime number calculation in progress";
            string sResultPrimeNumber = await t ;

            if (Token.IsCancellationRequested)
            {
                ProgressMessage = $"calculation cancelled @ result: {sResultPrimeNumber}";
            }
            else ProgressMessage = "....prime number calculation completed";
            return sResultPrimeNumber;
        }

        private async Task<string> GetAsyncLoopCount(char c)
        {
            ProgressMessage = "....starting calculation";

            Task<string> t = Task.Factory.StartNew<string>(() => 
                              {
                                  string concatenateString = String.Empty;
                                  int resultCount = 1;
                                  for (int i = 0; i < 200000; i++)
                                  {
                                      concatenateString += c;
                                      resultCount++;
                                      Count = resultCount;
                                      if(Count > 100000)
                                      {
                                          OnCancelCalculationTask();
                                          if (Token.IsCancellationRequested)
                                          {
                                              // clean-up work before cacellation
                                              Token.ThrowIfCancellationRequested();
                                          }
                                      }
                                  }
                                  return resultCount.ToString();
                              },Token);

            ProgressMessage = "....calculation in progress";

            string stotalcount = await t;

            ProgressMessage = "....calculation completed";
            return stotalcount;
        }
    }

    #region Custom EventArgs 
    public class CancelPrimeNumberCalculationEventArgs : EventArgs
    {
        public string Message { get; set; }
        public CancelPrimeNumberCalculationEventArgs(string message)
        {
            Message = message;
        }
    }

    public class CancelEventArgs : EventArgs
    {
        public int Zahl { get; set; }
        public CancelEventArgs(int zahl)
        {
            Zahl = zahl;
        }
    }
    #endregion

    //public class ViewModelBase : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected void OnChanged([CallerMemberName] string propertyName = "")
    //    {
    //        // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //}

}
