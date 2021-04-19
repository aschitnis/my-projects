namespace Abi.MySql.LibraryDatabase.Entities.Specifications
{
    public class MultipleSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first;
        private ISpecification<T> second;

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }
}
