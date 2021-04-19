using System;
using System.IO;
using System.Xml.Serialization;
using WpfNestedGridApp.datensatz;
using WpfNestedGridApp.interfaces;

namespace WpfNestedGridApp
{
    /************************************************** 
     * DIESE KLASSE IST NIRGENDS IN VERWENDUNG !!!
     *************************************************/
    //public class HubstatSingletonDataContainer : IDatensatzBeschreibung
    //{
    //    private CHubXmlMapping _hubstatXmlData = new CHubXmlMapping();

    //    private HubstatSingletonDataContainer()
    //    {
    //        XDocument xDoc = XDocument.Load(@"C:\Users\Mustermann\source\repos\ConsoleTestApp\WpfNestedGridApp\config\hubmap.xml");
    //        var ListxElements = xDoc.Elements("HUBSTAT").ToList<XElement>();

    //        _hubstatXmlData.HSatzVersandPartner.Start = ListxElements.Elements("hsatz").Elements("vp").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.HSatzVersandPartner.Length = ListxElements.Elements("hsatz").Elements("vp").Elements("length").ElementAt(0).Value;

    //        _hubstatXmlData.HSatzErstelldatum.Start = ListxElements.Elements("hsatz").Elements("erstelldatum").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.HSatzErstelldatum.Length = ListxElements.Elements("hsatz").Elements("erstelldatum").Elements("length").ElementAt(0).Value;

    //        _hubstatXmlData.ASatzSSCC.Start = ListxElements.Elements("asatz").Elements("nve").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.ASatzSSCC.Length = ListxElements.Elements("asatz").Elements("nve").Elements("length").ElementAt(0).Value;

    //        _hubstatXmlData.ASatzEmpfangsPartner.Start = ListxElements.Elements("asatz").Elements("ep").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.ASatzEmpfangsPartner.Length = ListxElements.Elements("asatz").Elements("ep").Elements("length").ElementAt(0).Value;

    //        _hubstatXmlData.ASatzDatum.Start = ListxElements.Elements("asatz").Elements("datum").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.ASatzDatum.Length = ListxElements.Elements("asatz").Elements("datum").Elements("length").ElementAt(0).Value;

    //        _hubstatXmlData.ASatzBemerkung.Start = ListxElements.Elements("asatz").Elements("bemerkung").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.ASatzBemerkung.Length = ListxElements.Elements("asatz").Elements("bemerkung").Elements("length").ElementAt(0).Value;

    //        _hubstatXmlData.ASatzKey.Start = ListxElements.Elements("asatz").Elements("key").Elements("start").ElementAt(0).Value;
    //        _hubstatXmlData.ASatzKey.Length = ListxElements.Elements("asatz").Elements("key").Elements("length").ElementAt(0).Value;
    //    }

    //    private static HubstatSingletonDataContainer instance = new HubstatSingletonDataContainer();
    //    public static HubstatSingletonDataContainer Instance => instance;

    //    public CHubXmlMapping GetHubMapping()
    //    {
    //        return _hubstatXmlData;
    //    }

    //    public CHubstatDatensatzBeschreibung GetDeserializedHubstat()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

namespace WpfNestedGridApp.singleton.datacontainer
{
    /**
     * Implementation of Singleton Pattern.
     * **/
    public class CHubstatXmlSerialization : IDatensatzBeschreibung
    {
        private CHubstatDatensatzBeschreibung _hubstatXmlData = new CHubstatDatensatzBeschreibung();

        #region Konstruktor
        private CHubstatXmlSerialization()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CHubstatDatensatzBeschreibung));

            string sXmlconfigFile = SingletonProgramConfiguration.Instance.GetConfigurationData()["hubstatxmlconfig"];

            TextReader reader = new StreamReader(sXmlconfigFile);
            _hubstatXmlData = (CHubstatDatensatzBeschreibung)deserializer.Deserialize(reader);
             reader.Close();
        }
        #endregion

        private static CHubstatXmlSerialization instance = new CHubstatXmlSerialization();
        public static CHubstatXmlSerialization Instance => instance;

        public CHubstatDatensatzBeschreibung GetDeserializedHubstat()
        {
            return _hubstatXmlData;
        }

        public CHubXmlMapping GetHubMapping()
        {
            throw new NotImplementedException();
        }
    }
}
