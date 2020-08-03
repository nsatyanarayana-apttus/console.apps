using System;
using System.Threading.Tasks;

namespace ServiceB.Service
{
    public interface IWebActorService
    {
        Task<T> Ask<T>(object message, TimeSpan? timeout = null);
    }
}
