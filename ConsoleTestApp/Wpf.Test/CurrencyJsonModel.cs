using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test
{
    public class CurrencyJsonModel : INotifyPropertyChanged
    {
        private string displayname;
        private string shortname;
        private string name;
        private string code;
        private string nameplural;

        public string ShortName { get { return shortname; } set { shortname = value; OnPropertyChanged(); } }
        public string DisplayName { get { return displayname; } set { displayname = value; OnPropertyChanged(); } }
        public string NamePlural { get { return nameplural; } set { nameplural = value;OnPropertyChanged(); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }
        public string Code { get { return code; } set { code = value; OnPropertyChanged(); } }

        public CurrencyJsonModel() {  }

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
