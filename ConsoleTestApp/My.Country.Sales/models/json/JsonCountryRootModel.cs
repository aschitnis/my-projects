using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.models.json
{
    public class JsonCountryRootModel 
    {
        private List<JsonCountryModel> jsoncountriesmodels;
        
        [JsonProperty("country")]
        public List<JsonCountryModel> JsonCountriesModels 
        {
            get { return jsoncountriesmodels; }
            set { jsoncountriesmodels = value; }
        }

        //#region INotifyChanged Event
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }
}
