using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleTestApp.Temp.Code
{
    [XmlRoot("rootlevel")]
    public class CTmpRoot : INotifyPropertyChanged
    {
        public CTmpRoot()
        {
            States = new CTmpStates();
        }
        private CTmpStates _states;

        [XmlArray("collection1")]
        [XmlArrayItem("state")]
        public CTmpStates States
        {
            get { return _states; }
            set { _states = value; OnPropertyChanged("States"); }
        }

        #region Event Notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }

    public class CTmpStates : List<CTmpState>
    {
        /*** ***/
    }

    public class CTmpState : INotifyPropertyChanged
    {
        public CTmpState()
        {
            Bezirke = new CTmpBezirke();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("population")]
        public int Population { get; set; }

        [XmlElement("latitude")]
        public double Latitude { get; set; }

        [XmlArray("bezirke")]
        [XmlArrayItem("bezirk")]
        public CTmpBezirke Bezirke { get; set; }

        #region Event Notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }

    public class CTmpBezirke : List<CTmpBezirk>
    {
        /**** ****/
    }

    public class CTmpBezirk : INotifyPropertyChanged
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("population")]
        public int Population { get; set; }

        [XmlElement("latitude")]
        public double Latitude { get; set; }

        [XmlElement("plz")]
        public int Plz { get; set; }

        #region Event Notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }

    public class CTmpXmlDeserialization
    {
        public CTmpRoot objectRoot { get; set; }

        public CTmpXmlDeserialization()
        {
            objectRoot = new CTmpRoot();
        }

        public void MakeDeserialization()
        {
            string xmltext = File.ReadAllText(@"C:\Users\Mustermann\source\repos\ConsoleTestApp\WpfNestedGridApp\BeispielDaten\tmpTesting.xml");
            using (TextReader textReader = new StringReader(xmltext))
            {
                 XmlSerializer serializer = new XmlSerializer(typeof(CTmpRoot));
                objectRoot = (CTmpRoot)serializer.Deserialize(textReader);
            }
        }

        #region Event Notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
