namespace Abi.MySql.LibraryDatabase.Entities.Specifications
{
    public class AuthorLastNameSpecification : ISpecification<Author>
    {
        private string authorLastName;

        #region Constructors
        public AuthorLastNameSpecification(string lastname)
        {
            this.authorLastName = lastname;
        }
        public AuthorLastNameSpecification() { }
        #endregion

        public bool IsSatisfied(Author t)
        {
            return t.LastName == authorLastName; 
        }
    }

    public class AuthorBirthYearSpecification : ISpecification<Author>
    {
        private int birthYear;

        #region Constructors
        public AuthorBirthYearSpecification(int birthyear)
        {
            this.birthYear = birthyear;
        }
        public AuthorBirthYearSpecification() { }
        #endregion

        public bool IsSatisfied(Author t)
        {
            return t.BirthYear == birthYear;
        }
    }
}
