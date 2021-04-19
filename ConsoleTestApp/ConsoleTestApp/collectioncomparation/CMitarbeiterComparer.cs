using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.collectioncomparation
{
    public enum MitarbeiterEnum
    {
        SalaryAscending,
        SalaryDescending,
        Age
    }
    public class CMitarbeiterComparer : IComparer<CMitarbeiter>
    {
        private MitarbeiterEnum mitarbeiterEigenschaftEnum { get; set; }

        public CMitarbeiterComparer(MitarbeiterEnum enumtype)
        {
            mitarbeiterEigenschaftEnum = enumtype;
        }
        public int Compare(CMitarbeiter first, CMitarbeiter second)
        {
            if (first == null && second == null) return 0;
            if (first == null) return -1;       // first < second
            else if (second == null) return 1;  // first > second

            int iresult = -1;
            switch(mitarbeiterEigenschaftEnum)
            {
                case MitarbeiterEnum.SalaryAscending:
                    if (first.Salary == second.Salary)
                        iresult = 0;
                    if (first.Salary > second.Salary)
                        iresult = 1;
                    else iresult = -1;
                    break;
                case MitarbeiterEnum.Age:
                    if (first.Age == second.Age)
                        iresult = 0;
                    if (first.Age > second.Age)
                        iresult = 1;
                    else iresult = -1;
                    break;
                case MitarbeiterEnum.SalaryDescending:
                    if (second.Salary == first.Salary)
                        iresult = 0;
                    if (second.Salary > first.Salary)
                        iresult = 1;
                    else iresult = -1;
                    break;
                default:
                    throw new ArgumentException("unexpected Comparetype");
            }
            return iresult;
        }
    }
}
