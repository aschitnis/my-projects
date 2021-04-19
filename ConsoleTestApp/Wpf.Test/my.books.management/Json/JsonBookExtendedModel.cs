using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management
{
    [Serializable]
    public class JsonBookExtendedModel
    {
        [JsonProperty]
        public int BookId { get; set; }

        [JsonProperty]
        public List<string> Indexes { get; set; }

        public JsonBookExtendedModel()
        {
            Indexes = new List<string>();
        }
    }
}
