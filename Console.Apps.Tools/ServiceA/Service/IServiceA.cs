using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceA.Service
{
    public interface IServiceA
    {
        Task<string> GetServiceAMessageAsync(string id);
    }
}
