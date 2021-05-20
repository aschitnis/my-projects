using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.websocket.client.classes;

namespace wpf.websocket.client
{
    public class SocketViewModel : ViewModelBase
    {
        private List<string> currenciesnames;
        private string selectedcurrencyname;
        private CurrenciesData CurrenciesStructDataModel { get; set; } = new CurrenciesData();

        public List<string> CurrenciesNames { get => currenciesnames ?? default ; set { currenciesnames = value; OnPropertyChanged(); } }
        public string SelectedCurrencyName { get => selectedcurrencyname; set { selectedcurrencyname = value; OnPropertyChanged(); } }
        private List<string> CurrencyCodes { get; set; } = new List<string>();

        public SocketViewModel()
        {
            Init();
        }

        private void Init()
        {
            CurrenciesStructDataModel = new CurrenciesData();
            CurrenciesNames = new List<string>();
            CurrenciesStructDataModel.Currencies.ForEach((c) => { CurrenciesNames.Add(c.Name);CurrencyCodes.Add(c.Code); }) ;
        }

        public string GetSelectedCurrencyCode()
        {
            return CurrenciesStructDataModel.Currencies.Where(c => c.Name == SelectedCurrencyName).FirstOrDefault().Code;
        }
    }
}
