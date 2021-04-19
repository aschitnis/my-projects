using System.Collections.Generic;
using System.Linq;

namespace ConsoleTest
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public User()
        {

        }
    }

    internal class UserLastNameAndBirthYearEqualityComparer : EqualityComparer<User>
    {
        public override bool Equals(User x, User y)
        {
            return (x.LastName == y.LastName) && (x.BirthYear == y.BirthYear);
        }

        public override int GetHashCode(User obj)
        {
            return obj == null ? 0 : obj.BirthYear;
        }
    }
    public class PersonalManagement
    {
        public List<User> Users { get; set; }
        public List<PersonalInfo> PersonalListe { get; set; }
        public PersonalManagement()
        {
            PersonalListe = new List<PersonalInfo>();
            Users = new List<User>()
            {
                new User() { FirstName="Abhijit", LastName="Chitnis", BirthYear=1971},
                new User() { FirstName="Raju", LastName="Thakurdesai", BirthYear=1964},
                new User() { FirstName="Gopal", LastName="Joshi", BirthYear=1954},
                new User() { FirstName="Naresh", LastName="Sahu", BirthYear=1982},
                new User() { FirstName="Golu", LastName="Mishra", BirthYear=1986},
                new User() { FirstName="Arnold", LastName="Angerer", BirthYear=1972},
                new User() { FirstName="Markus", LastName="Hochradl", BirthYear=1982},
                new User() { FirstName="Jutta", LastName="Petermaier", BirthYear=1981},
                new User() { FirstName="Christoph", LastName="Reichl", BirthYear=1969},
                new User() { FirstName="Jurgen", LastName="Bauer", BirthYear=1984},

                new User() { FirstName="Robert", LastName="Kienberger", BirthYear=1974},
                new User() { FirstName="Naresh", LastName="Ladda", BirthYear=1976},
                new User() { FirstName="Robert", LastName="Kronberger", BirthYear=1968},
                new User() { FirstName="Gitti", LastName="Özgülüm", BirthYear=1990},
                new User() { FirstName="Armin", LastName="Tirler", BirthYear=1968},
                new User() { FirstName="Martha", LastName="Rettenbacher", BirthYear=1982},
                new User() { FirstName="Gopal", LastName="Thakurdesai", BirthYear=1964},
                new User() { FirstName="Prakash", LastName="Gupta", BirthYear=1980},
                new User() { FirstName="Deepak", LastName="Gupta", BirthYear=1966},
                new User() { FirstName="Anil", LastName="Jha", BirthYear=1961}
            };
        }

        public void GetAllDistinctStaffMembersUsingGroupBy()
        {
           var usrs = Users.GroupBy(o => o.BirthYear).Select(g => g).Select(w => w.AsEnumerable()) ;
        }
        public void GetAllDistinctStaffMembersUsingEqualityOperator(out List<PersonalInfo> personallist)
        {
            personallist = Users.Distinct(new UserLastNameAndBirthYearEqualityComparer()).Select(x => new PersonalInfo() { VorName = x.FirstName, NachName = x.LastName }).ToList();
        }
    }
}
