using System;
using System.Linq.Expressions;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}
