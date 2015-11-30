using System;

namespace Quilt4.Service.SqlRepository.Interface
{
    internal interface IReadOnlyContext<out TContext>
    {
        T Execute<T>(Func<TContext, T> func);
    }
}