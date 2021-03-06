using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.Generic.Template
{
    public class JsonDocument : IDocument
    {
        #region Constructor
        public JsonDocument()
        {
        }

        public JsonDocument(string title, string format)
        {
            this.Title = title;
            this.Format = format;
        }
        #endregion

        public string Title { get; set; }
        public string Format { get; set; }
    }
}
