using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Extensions
{
    public static class ArbeiterExtension
    {
        public static string GetWorkersModelCategory(this IArbeiterModel workermodel)
        {
            if (workermodel is LeiharbeiterModel)
                return $"{(workermodel as LeiharbeiterModel).Name} -- Leiharbeiter";
            else if (workermodel is AngestellteModel)
                return $"{(workermodel as AngestellteModel).Name} -- Angestellte";
            else return null;
        }
    }
}
