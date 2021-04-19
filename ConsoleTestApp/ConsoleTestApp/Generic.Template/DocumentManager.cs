using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.Generic.Template
{
    public class DocumentManager<TDocument>
                    where TDocument: Template.IDocument
    {
        public List<Template.IDocument> Documents { get; set; }

        public DocumentManager()
        {
            Documents = new List<Template.IDocument>();
            Documents.Add(new TextDocument("baukosten.txt", "text"));
            Documents.Add(new PdfDocument("notstandhilfe.pdf", "pdf"));
            Documents.Add(new XmlDocument("what.xml", "xml"));
            Documents.Add(new JsonDocument("graphhopper.json", "json"));
            Documents.Add(new TextDocument("iso-codes.txt", "text"));
        }

        public void DisplayAllDocuments()
        {
            foreach( TDocument doc in Documents)
            {
                Console.WriteLine(doc.Title + " - "+ doc.Format);
            }
        }
    }
}
