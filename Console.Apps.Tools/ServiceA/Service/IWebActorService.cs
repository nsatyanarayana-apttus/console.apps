using System;
using System.Threading.Tasks;

namespace ServiceA.Service
{
    public interface IWebActorService
    {
        Task<T> Ask<T>(object message, TimeSpan? timeout = null);
    }
}
