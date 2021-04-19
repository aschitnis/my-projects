using WpfNestedGridApp.xml;
using WpfNestedGridApp.xml.model;

namespace WpfNestedGridApp.viewmodels.xml
{
    public class CBundRootViewModel : ViewModelBase
    {
        private IBundService oService { get; set; }
        
        public CBundRootViewModel(IBundService service) : this()
        {
            oService = service;
        }
        public CBundRootViewModel()
        {
            RootModel = new CBundRootModel();
        }

        #region properties
        private CBundRootModel _rootmodel;

        public CBundRootModel RootModel
        {
            get { return _rootmodel; }
            set { SetProperty(ref _rootmodel, value); }
        }

        private void LoadFromXml()
        {
           if (oService.IstDateiVorhanden())
            {
               oService.Deserialize();
            }
        }
        #endregion
    }
}
