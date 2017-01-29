namespace Data.Queries
{
    using System.Collections.Generic;

    public interface IRepository<out TResult>
    {
        IEnumerable<TResult> Execute(IQuery query);
    }
}