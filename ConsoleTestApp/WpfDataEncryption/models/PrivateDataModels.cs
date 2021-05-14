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

    public class TopicModel : ViewModelBase
    {
        private string _name;
        private UserModel _userdata;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public UserModel UserData { get => _userdata ?? new UserModel(); set => SetProperty(ref _userdata, value); }
        
        #region constructor
        public TopicModel(string name, UserModel usrdata) { UserData = usrdata; Name = name; }
        public TopicModel() {  }
        #endregion
    }

    public class UserModel : ViewModelBase
    {
        private string _name;
        private string _password;
        private string _additionaldata;
        private string _weblink;
        private string _securitydata;
        private string _debitcard;
        private BranchInformation _bankbranchdetails;
        private ExtraSecurityData _additionalsecuritydata;

        public string Name { get => _name; set => SetProperty(ref _name, value); } 
        public string Password { get => _password; set => SetProperty(ref _password, value); } 
        public string SecurityData { get => _securitydata; set => SetProperty(ref _securitydata, value); }
        public string AdditionalData { get => _additionaldata; set => SetProperty(ref _additionaldata, value); } 
        public string Weblink { get => _weblink; set => SetProperty(ref _weblink, value); } 
        public ExtraSecurityData AdditionalSecurityData { get => _additionalsecuritydata ?? new ExtraSecurityData();  set => SetProperty(ref _additionalsecuritydata, value); } 
        public string DebitCard { get => _debitcard; set => SetProperty(ref _debitcard, value); }
        public BranchInformation BankBranchDetails { get => _bankbranchdetails ?? new BranchInformation(); set => SetProperty(ref _bankbranchdetails, value); }
        public UserModel() {  }

        internal void Init(string name, string password, string securitydata, string addtdata,string weblink,ExtraSecurityData addtsecuritydata)
        {
            Name = name;
            Password = password;
            SecurityData = securitydata;
            AdditionalData = addtdata;
            Weblink = weblink;
            AdditionalSecurityData =  addtsecuritydata;
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

    public class BranchInformation:ViewModelBase
    {
        private string _telefon;
        private string _code;
        private string _name;
        private string _address;

        public string Telefon { get => _telefon; set=> SetProperty(ref _telefon, value); }
        public string Code { get => _code; set => SetProperty(ref _code, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Address { get => _address; set => SetProperty(ref _address, value); }
        
        #region constructor
        public BranchInformation(string telefon,string code,string name,string address)
        {
            Telefon = telefon;
            Code = code;
            Name = name;
            Address = address;
        }
        public BranchInformation() {}
        #endregion
    }
}
