using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.http.service.currency
{
    public class Person : ViewModelBase
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public Person() {  }
    }

    public class PersonViewModel : ViewModelBase
    {
        public List<Person> PersonsList { get; set; }
        public PersonViewModel()
        {
            PersonsList = new List<Person>();
            PersonsList.Add(new Person { Vorname = "Abhijit", Nachname = "Chitnis" });
            PersonsList.Add(new Person { Vorname = "Wolfgang", Nachname = "Heindl" });
            PersonsList.Add(new Person { Vorname = "Markus", Nachname = "Hochradl" });
            PersonsList.Add(new Person { Vorname = "Mohammed", Nachname = "Shartouh" });
        }
    }
}
