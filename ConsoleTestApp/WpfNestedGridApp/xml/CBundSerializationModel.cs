using System.Collections.Generic;
using System.Xml.Serialization;

namespace WpfNestedGridApp.klasse.schema.xml
{
    #region Root Class
    [XmlRoot("data")]
    public class CBundSerializationModel 
    {
        public CBundSerializationModel()
        {
            Bundeslaender = new CXmlBundeslaenderModel();
        }
        private CXmlBundeslaenderModel bundeslaender;
        [XmlArray("bundeslaender")]
        [XmlArrayItem("bund")]
        public CXmlBundeslaenderModel Bundeslaender
        {
            get { return bundeslaender; }
            set { bundeslaender = value; }
        }
    }
    #endregion


    /************************** Container Classes   *********************************************************/
    public class CXmlBundeslaenderModel : List<CXmlBundModel>
    {

    }

    public class CXmlBezirkeModel : List<CXmlBezirkModel>
    {
        
    }

    public class CXmlGemeindenModel : List<CXmlGemeindeModel>
    {

    }

    public class CXmlOrteModel : List<CXmlOrtModel>
    {

    }
    /***************************************************************************************/


    public class CXmlGemeindeModel 
    {
        public CXmlGemeindeModel()
        {
            ListOrte = new CXmlOrteModel();
        }
        [XmlElement("plz")]
        public string Plz { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("einwohner")]
        public int Einwohner { get; set; }
        [XmlElement("lat")]
        public string Lat { get; set; }
        [XmlElement("lon")]
        public string Lon { get; set; }
        [XmlElement("flaeche")]
        public string Flaeche { get; set; }
        [XmlElement("bezeichnung")]
        public string Bezeichnung { get; set; }

        [XmlArray("orte")]
        [XmlArrayItem("ort")]
        public CXmlOrteModel ListOrte
        {
            get;
            set;
        }
    }

    public class CXmlBezirkModel
    {
        public CXmlBezirkModel()
        {
            ListGemeinde = new CXmlGemeindenModel();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("bezeichnung")]
        public string Bezeichnung { get; set; } // entspricht typ
        [XmlElement("plz")]
        public string Plz { get; set; }
        [XmlElement("einwohner")]
        public int Einwohner { get; set; }
        [XmlElement("lat")]
        public string Lat { get; set; }
        [XmlElement("lon")]
        public string Lon { get; set; }
        [XmlElement("flaeche")]
        public string Flaeche { get; set; }
        [XmlArray("gemeinden")]
        [XmlArrayItem("gemeinde")]
        public CXmlGemeindenModel ListGemeinde { get; set; }
    }

    public class CXmlBundModel
    {
        public CXmlBundModel()
        {
            ListBezirke = new CXmlBezirkeModel();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("einwohner")]
        public int Einwohner { get; set; }
        [XmlElement("lat")]
        public string Lat { get; set; }
        [XmlElement("lon")]
        public string Lon { get; set; }
        [XmlArray("bezirke")]
        [XmlArrayItem("bezirk")]
        public CXmlBezirkeModel ListBezirke { get; set; }
    }

    public class CXmlOrtModel
    {
        public CXmlOrtModel()
        {

        }
        [XmlElement("bezeichnung")]
        public string Bezeichnung { get; set; } // entspricht typ
        [XmlElement("plz")]
        public string Plz { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("einwohner")]
        public int Einwohner { get; set; }
        [XmlElement("lat")]
        public string Lat { get; set; }
        [XmlElement("lon")]
        public string Lon { get; set; }
        [XmlElement("flaeche")]
        public string Flaeche { get; set; }
        [XmlElement("hoehe")]
        public int Hoehe { get; set; }
        [XmlElement("lage")]
        public string Lage { get; set; }
        [XmlElement("verkehr")]
        public string Verkehr { get; set; }
    }
}
