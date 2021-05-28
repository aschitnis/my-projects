using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfDataEncryption.models
{

    /// <summary>
    // ICloneable:MemberwiseClone()
    //  - If a field is a reference type, then the only reference is copied not the referred object.
    //      So here, the cloned object and the original object will refer to the same object.
    //  - If the field is value type then the bit-by-bit copy of the field will be performed.
   /// </summary>
    [XmlRoot("data")]
    public class XmlObjectModel : ICloneable
    {
        [XmlArray("topics")]
        [XmlArrayItem("topic")]
        public XmlTopics TopicsList { get; set; }

        public virtual object Clone()
        {
            XmlObjectModel result = (XmlObjectModel)this.MemberwiseClone();
            XmlObjectModel clonedResult = new XmlObjectModel(new XmlTopics());
            result.TopicsList.ForEach(t => { clonedResult.TopicsList.Add(t.Clone() as XmlTopic); });
            
            //XmlObjectModel clonedResult = new XmlObjectModel(new Topics().ForEach( Topic));
            //clonedResult.TopicsList = new Topics();
            //XmlObjectModel result = (XmlObjectModel)this.MemberwiseClone();
            //Topics clonedTopics = new Topics();
            //foreach (Topic t in result.TopicsList)
            //{
            //    clonedTopics.Add(t.Clone() as Topic);
            //}
            //clonedResult.TopicsList.AddRange(clonedTopics);
            
            return clonedResult;
        }

        public XmlObjectModel() { }

        public XmlObjectModel(XmlTopics topics)
        {
            TopicsList = topics;
        }
    }


    public class XmlTopics : List<XmlTopic>  {}

    public class XmlTopic : ICloneable
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("user")]
        public XmlUser User { get; set; }
        public virtual object Clone()
        {
            XmlTopic topic = (XmlTopic)this.MemberwiseClone();
            //topic.User = (User)topic.User.Clone();
            return topic;
        }
    }

    public class XmlUser : ICloneable
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
        [XmlElement("extrasecuritydata")]
        public XmlExtraSecurityData AdditionalSecurityData { get; set; }
        //[XmlElement("debitcard")]
        //public string DebitCard { get; set; }
        //[XmlElement("branchDetails")]
        //public BranchDetails BranchInformation { get; set; }

        public virtual object Clone()
        {
            XmlUser usr = (XmlUser)this.MemberwiseClone();
            // usr.AdditionalSecurityData = (ExtraSecurityData)usr.AdditionalSecurityData.Clone();
            return usr;
        }
    }

    public class XmlBranchDetails
    {
        [XmlElement("branchPhone")]
        public string Telefon { get; set; }
        [XmlElement("branchCode")]
        public string Code { get; set; }
        [XmlElement("branchName")]
        public string Name { get; set; }
        [XmlElement("branchAddress")]
        public string Address { get; set; }
    }

    public class XmlExtraSecurityData : ICloneable
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
        public virtual object Clone()
        {
            XmlExtraSecurityData addtdata = (XmlExtraSecurityData)this.MemberwiseClone();
            return addtdata;
        }
    }
}
