using System;
using System.IO;
using System.Xml.Serialization;
using WpfNestedGridApp.klasse.schema.xml;

namespace WpfNestedGridApp.xml
{
    public class CBundSerializationService : IBundService
    {
        public string sOutfile { get; set; }
        public bool IstErrorVorhanden { get; set; }
        public CBundSerializationServiceException SerializationExceptionObject { get; set; }

        public CBundSerializationModel objXml { get; private set; }

        public CBundSerializationService()
        {
            sOutfile = SingletonProgramConfiguration.Instance.GetConfigurationData()["bundxmldata"];
        }

        public void Deserialize()
        {
            string xmldata = File.ReadAllText(sOutfile);
            using (TextReader textReader = new StringReader(xmldata))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CBundSerializationModel));
                try
                {
                    objXml = (CBundSerializationModel)serializer.Deserialize(textReader);
                }
                catch(Exception ex)
                {
                    if(ex.InnerException == null)
                    {
                        SerializationExceptionObject = new CBundSerializationServiceException(ex.Message, ex.InnerException);
                    }
                    else
                    {
                        SerializationExceptionObject = new CBundSerializationServiceException(ex.Message);
                    }
                }
            }
        }

        public bool IstDateiVorhanden()
        {
            if (!File.Exists(sOutfile))
            {
                SerializationExceptionObject = new CBundSerializationServiceException();
                SerializationExceptionObject.FileNotFound = true;
                SerializationExceptionObject.ErrorMessage = string.Format("Datei:{0} nicht gefunden", sOutfile);
                return false;
            }
            return true;
        }
    }

    public class CBundSerializationServiceException : Exception
    {
        public bool FileNotFound { get; set; }
        public string ErrorMessage { get; set; }
        public Exception InnerError { get; set; }

        public CBundSerializationServiceException()
        {
            FileNotFound = false;
        }

        public CBundSerializationServiceException(string message):this()
        {
            ErrorMessage = message;
        }

        public CBundSerializationServiceException(string message, Exception inner):this()
        {
            ErrorMessage = message;
            InnerError = inner;
        }
    }
}
