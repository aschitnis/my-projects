using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.websocket.client.Properties;

namespace wpf.websocket.client.classes
{
    public class CurrenciesData 
    {
        internal List<Currency> Currencies { get; set; } 
        internal static string CURRENCIESPATH { get; private set; }
        public CurrenciesData()
        {
            Currencies = new List<Currency>();
            Init();
        }

        private void Init()
        {
            CURRENCIESPATH = this.GetType().Assembly.Location.Substring(0, this.GetType().Assembly.Location.LastIndexOf(@"\")) + Settings.Default.Currencies; ;
            
            if (File.Exists(CURRENCIESPATH))
            {
                string jsonString = File.ReadAllText(CURRENCIESPATH);
                Newtonsoft.Json.Linq.JToken tokencontainer = Newtonsoft.Json.Linq.JObject.Parse(jsonString);

                tokencontainer.Children().ToList().ForEach((t) =>
                {
                   string name = (string)t.First.Parent.First.SelectToken("name");
                    string code = (string)t.First.Parent.First.SelectToken("code");
                   Currency currency = new Currency(code, name);
                   Currencies.Add(currency);
                });
            }
        }
    }

    public struct Currency
    {
        public string Code;
        public string Name;
        public Currency(string _code, string _name)
        {
            this.Code = _code;
            this.Name = _name;
        }
    }
}
