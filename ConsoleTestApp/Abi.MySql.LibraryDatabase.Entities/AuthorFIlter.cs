using System.Collections.Generic;

namespace Abi.MySql.LibraryDatabase.Entities
{
    public class AuthorFilter : IFilter<Author>
    {
        public IEnumerable<Author> Filter(IEnumerable<Author> authors, ISpecification<Author> spec)
        {
            foreach(Author aut in authors)
            {
                if (spec.IsSatisfied(aut))
                {
                    yield return aut;
                }
            }
        }
    }
}
