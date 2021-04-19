using System.Collections.Generic;

namespace Abi.MySql.LibraryDatabase.Entities
{
    // https://blog.usejournal.com/net-design-pattern-3b747d155588
    // using the Open.Closed Principle
    public class Book
    {
        private int _bookid;

        public int BookId
        {
            get { return _bookid; }
            set { _bookid = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private List<Author> authorsList;

        public List<Author> AuthorsList
        {
            get { return authorsList; }
            set { authorsList = value; }
        }

        private Country _countryObject;

        public Country CountryObject
        {
            get { return _countryObject; }
            set { _countryObject = value; }
        }

    }

    #region Author
    public class Author
    {
        private int _authorid;

        public int AuthorId
        {
            get { return _authorid; }
            set { _authorid = value; }
        }

        private string _firstname;
        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        private string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
        private int _birthyear;

        public int BirthYear
        {
            get { return _birthyear; }
            set { _birthyear = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
    #endregion

    #region Country
    public class Country
    {
        private int _countryid;

        public int CountryId
        {
            get { return _countryid; }
            set { _countryid = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
    #endregion
}
