using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.events
{
    public class CKontoTransaktion
    {
        public int Sollstand { get; set; } = 0; // Belastungen, Verbindlichkeiten usw.. verbucht
        public int Habenstand { get; set; } = 0; // Kontostand, Vorräte
        public int Saldo { get; set; } = 0; // bestand eines Kontos

        public CKontoTransaktion()
        {
        }
    }
}
