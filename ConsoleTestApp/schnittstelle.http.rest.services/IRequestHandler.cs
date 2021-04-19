using System.Collections.Generic;

namespace schnittstelle.http.rest.services
{
    public interface IRequestHandler
    {
        string HttpUrl { get; set; }
        void PostDataToValidate(string url);
        Dictionary<int, string> HttpValidationStatusMsgDictionary { get; set; }
    }
}
