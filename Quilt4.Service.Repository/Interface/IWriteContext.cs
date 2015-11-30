using System;

namespace Quilt4.Service.SqlRepository.Interface
{
    internal interface IWriteContext<out TContext>
    {
        void Execute(Action<TContext> action);
        T Execute<T>(Func<TContext, T> func);
    }
}