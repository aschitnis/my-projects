using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfDataEncryption.classes
{
    [XmlRoot("data")]
    public class XmlObjectModel : ICloneable
    {
        [XmlArray("topics")]
        [XmlArrayItem("topic")]
        public Topics TopicsList { get; set; }

        public virtual object Clone()
        {
            XmlObjectModel result = (XmlObjectModel)this.MemberwiseClone();
            XmlObjectModel clonedResult = new XmlObjectModel(new Topics());
            result.TopicsList.ForEach(t => { clonedResult.TopicsList.Add(t.Clone() as Topic); });
            
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

        public XmlObjectModel(Topics topics)
        {
            TopicsList = topics;
        }
    }


    public class Topics : List<Topic>  {}

    public class Topic : ICloneable
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("user")]
        public User User { get; set; }
        public virtual object Clone()
        {
            Topic topic = (Topic)this.MemberwiseClone();
            //topic.User = (User)topic.User.Clone();
            return topic;
        }
    }

    public class User : ICloneable
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
        public ExtraSecurityData AdditionalSecurityData { get; set; }
        //[XmlElement("debitcard")]
        //public string DebitCard { get; set; }
        //[XmlElement("branchDetails")]
        //public BranchDetails BranchInformation { get; set; }

        public virtual object Clone()
        {
            User usr = (User)this.MemberwiseClone();
            // usr.AdditionalSecurityData = (ExtraSecurityData)usr.AdditionalSecurityData.Clone();
            return usr;
        }
    }

    public class BranchDetails
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

    public class ExtraSecurityData : ICloneable
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
            ExtraSecurityData addtdata = (ExtraSecurityData)this.MemberwiseClone();
            return addtdata;
        }
    }
}
