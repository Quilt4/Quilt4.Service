using System;

namespace Quilt4.Service.SqlRepository.Data
{
    internal interface IReadOnlyContext<out TContext>
    {
        T Execute<T>(Func<TContext, T> func);
    }
}