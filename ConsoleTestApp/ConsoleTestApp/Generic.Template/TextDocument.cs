using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.Generic.Template
{
    public class TextDocument : IDocument
    {
        #region Constructor
        public TextDocument()
        {
        }

        public TextDocument(string title, string format)
        {
            this.Title = title;
            this.Format = format;
        }
        #endregion

        public string Title { get; set; }
        public string Format { get; set; }
    }
}
