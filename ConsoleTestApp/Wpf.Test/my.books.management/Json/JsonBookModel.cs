using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management
{
    [Serializable]
    public class JsonBookModel
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Language { get; set; }
        [JsonProperty]
        public string Publisher { get; set; }
        [JsonProperty]
        public string Author { get; set; }
        [JsonProperty]
        public string Information { get; set; }
    }
}
