using System;

namespace Quilt4.Service.SqlRepository.Data
{
    internal interface IWriteContext<out TContext>
    {
        void Execute(Action<TContext> action);
    }
}