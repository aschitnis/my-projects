using System.Xml.Serialization;

namespace WpfNestedGridApp.datensatz
{
    [XmlRoot("HUBSTAT")]
    public class CHubstatDatensatzBeschreibung
    {
        [XmlElement("hsatz")]
        public CHsatz Hsatz { get; set; }
        [XmlElement("asatz")]
        public CAsatz Asatz { get; set; }
    }

    public class CHsatz
    {
        [XmlElement("vp")]
        public CVersanddatum Vp { get; set; }
        [XmlElement("erstelldatum")]
        public CErstelldatum Erstelldatum { get; set; }
    }

    public class CVersanddatum
    {
        public string start { get; set; }
        public string length { get; set; }
    }

    public class CErstelldatum
    {
        public string start { get; set; }
        public string length { get; set; }
    }

    public class CAsatz
    {
        [XmlElement("key")]
        public CKey Key { get; set; }
        [XmlElement("ep")]
        public CEmpfangspartner Ep { get; set; }
        [XmlElement("nve")]
        public CNve Nve { get; set; }
        [XmlElement("datum")]
        public CDatum Datum { get; set; }
        [XmlElement("bemerkung")]
        public CBemerkung Bemerkung { get; set; }
    }

    public class CKey
    {
        public string start { get; set; }
        public string length { get; set; }
    }

    public class CNve
    {
        public string start { get; set; }
        public string length { get; set; }
    }

    public class CEmpfangspartner
    {
        public string start { get; set; }
        public string length { get; set; }
    }

    public class CDatum
    {
        public string start { get; set; }
        public string length { get; set; }
    }

    public class CBemerkung
    {
        public string start { get; set; }
        public string length { get; set; }
    }
}
