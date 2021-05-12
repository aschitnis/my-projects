using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfDataEncryption.classes;
using WpfDataEncryption.viewmodels;

namespace WpfDataEncryption.models
{
    public class ModelsContainer : ViewModelBase
    {
        private List<TopicModel> _topics;
        private List<TopicModel> _filteredtopics;

        // Name => _name != null ? _name : "NA";
        public List<TopicModel> Topics  
         { get => _topics != null ? _topics : new List<TopicModel>(); private set => SetProperty(ref _topics, value);  }
        public List<TopicModel> FilteredTopics { get => _filteredtopics != null ? _filteredtopics : new List<TopicModel>(); set => SetProperty(ref _filteredtopics, value);  }
        public ModelsContainer() {   }

        private void Initialize()
        {
            // XmlManager xmlManager
        }
    }

    public class TopicModel : ViewModelBase
    {
        private string _name;
        private UserModel _userdata;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public UserModel UserData { get { return _userdata; } set { SetProperty(ref _userdata, value); } }
        public TopicModel(string name, UserModel usrdata)
        {
           // UserData = usrdata ?? default(UserModel);
        }
        public TopicModel()
        {
            UserData = new UserModel();
        }
    }

    public class UserModel : ViewModelBase
    {
        private string _name;
        private string _password;
        private string _additionaldata;
        private string _weblink;
        private string _securitydata;
        private ExtraSecurityData _additionalsecuritydata;

        public string Name { get { return _name; } set { SetProperty(ref _name, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }
        public string SecurityData { get { return _securitydata; } set { SetProperty(ref _securitydata, value); } }
        public string AdditionalData { get { return _additionaldata; } set { SetProperty(ref _additionaldata, value); } }
        public string Weblink { get { return _weblink; } set { SetProperty(ref _weblink, value); } }
        public ExtraSecurityData AdditionalSecurityData { get { return _additionalsecuritydata; } set { SetProperty(ref _additionalsecuritydata, value); } }

        public UserModel() {  }

        internal void Init(string name, string password, string securitydata, string addtdata,string weblink,ExtraSecurityData addtsecuritydata)
        {
            AdditionalSecurityData = addtsecuritydata == null ? new ExtraSecurityData() : addtsecuritydata;
        }
    }

    public class ExtraSecurityData
    {
        private string _number;
        private string _customerpassword;
        private string _pin;
        private string _additionalpin;
        private string _puk;

        public string Number { get { return _number; } set { _number = value; } }
        public string CustomerPassword { get { return _customerpassword; } set { _customerpassword = value; } }
        public string Pin { get { return _pin; } set { _pin = value; } }
        public string AdditionalPin { get { return _additionalpin; } set { _additionalpin = value; } }
        public string Puk { get { return _puk; } set { _puk = value; } }
        public ExtraSecurityData(string number,string password,string pin,string additionalpin,string puk)
        {
            Number = number;
            CustomerPassword = password;
            Pin = pin;
            AdditionalPin = additionalpin;
            Puk = puk;
        }
        public ExtraSecurityData()  {  }
    }
}
