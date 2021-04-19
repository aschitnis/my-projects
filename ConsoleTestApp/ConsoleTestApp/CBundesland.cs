using System.Collections.Generic;

namespace ConsoleTestApp
{

    /*********************************************************************************************************************************************************************************/
    public class CBundesland
    {
        public string Name { get; set; }
        public string Bezeichnung { get; set; }  // entspricht typ
        public int Einwohner { get; set; } 
        public double Lat { get; set; }
        public double Lon { get;set; }
        public double Flaeche { get; set; }
        public List<string> ListPostleitzahl { get; set; }
        public List<CBezirk> ListBezirke { get; set; }
        public CBundesland()
        {
            ListBezirke = new List<CBezirk>();
            ListPostleitzahl = new List<string>();
        }
    }


    public class CBezirk
    {
        public string Name { get; set; }
        public string Bezeichnung { get; set; } // entspricht typ
        public string Plz { get; set; }
        public int Einwohner { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Flaeche { get; set; }
        public List<string> ListPostleitzahl { get; set; }
        public List<CGemeinde> ListGemeinde { get; set; }
        public CBezirk()
        {
            ListGemeinde = new List<CGemeinde>();
            ListPostleitzahl = new List<string>();
        }
    }
    public class CGemeinde
    {
        public string Plz { get; set; }
        public string Name { get; set; }
        public int Einwohner { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Flaeche { get; set; }
        public List<string> ListPostleitzahl { get; set; }
        public string Bezeichnung { get; set; } // entspricht typ
        public List<COrt> ListOrte { get; set; }

        public CGemeinde()
        {
            ListOrte = new List<COrt>();
            ListPostleitzahl = new List<string>();
        }
    }

    public class COrt
    {
        public string Bezeichnung { get; set; } // entspricht typ
        public string Plz { get; set; }
        public string Name { get; set; }
        public int Einwohner { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Flaeche { get; set; }
        public int Hoehe { get; set; }
        public string Lage { get; set; }
        public string Verkehr { get; set; }
        public List<string> ListPostleitzahl { get; set; }
        public COrt()
        {
            ListPostleitzahl = new List<string>();
        }
    }
}
