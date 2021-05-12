using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfDataEncryption.classes
{
    [XmlRoot("data")]
    public class XmlObjectModel 
    {
        [XmlArray("topics")]
        [XmlArrayItem("topic")]
        public Topics TopicsList { get; set; }
    }


    public class Topics : List<Topic>  { }

    public class Topic
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("user")]
        public User User { get; set; }

    }

    public class User
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("password")]
        public string Password { get; set; }
        [XmlElement("securitydata")]
        public string SecurityData { get; set; }
        [XmlElement("addtdata")]
        public string AdditionalData { get; set; }
        [XmlElement("web")]
        public string Weblink { get; set; }
        public ExtraSecurityData AdditionalSecurityData { get; set; }
    }

    public class ExtraSecurityData
    {
        [XmlElement("nr")]
        public string Number { get; set; }
        [XmlElement("kundenkennwort")]
        public string CustomerPassword { get; set; }
        [XmlElement("pin")]
        public string Pin { get; set; }
        [XmlElement("zusatzpin")]
        public string AdditionalPin { get; set; }
        [XmlElement("puk")]
        public string Puk { get; set; }
    }
}
