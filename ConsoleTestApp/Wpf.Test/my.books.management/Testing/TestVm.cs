using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management.Testing
{
    public class TestIndexModel : INotifyPropertyChanged
    {
        private string _indexinformation;
        public string IndexInformation 
        {
            get { return _indexinformation; }
            set { _indexinformation = value; OnChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class TestVm : INotifyPropertyChanged
    {
        private TestIndexModel _selecteddata;

        private ObservableCollection<TestIndexModel> _indexmodelslist;

        public ObservableCollection<TestIndexModel> IndexModelsList
        {
            get { return _indexmodelslist ?? (_indexmodelslist = new ObservableCollection<TestIndexModel>()); }
            set { _indexmodelslist = value;  }
        }
        public TestIndexModel SelectedData
        {
            get { return _selecteddata; }
            set
            {
                _selecteddata = value;
                OnChanged();
            }
        }

        internal void FillCollectionWithIndexData()
        {
            IndexModelsList.Add(new TestIndexModel() { IndexInformation = "The sound of the heart Page 77" });
            IndexModelsList.Add(new TestIndexModel() { IndexInformation = "The sound of the heart_002 Page 05" });
            IndexModelsList.Add(new TestIndexModel() { IndexInformation = "The sound of the heart_003 Page 121" });
            IndexModelsList.Add(new TestIndexModel() { IndexInformation = "The sound of the heart_004 Page 721" });
            IndexModelsList.Add(new TestIndexModel() { IndexInformation = "The sound of the heart_005 Page 14" });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
