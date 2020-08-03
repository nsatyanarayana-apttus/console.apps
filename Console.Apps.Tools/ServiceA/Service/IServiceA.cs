using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceA.Service
{
    public interface IServiceA
    {
        Task<string> GetServiceAMessageTest1Async(string id);
        Task<string> GetServiceAMessageTest2Async(string id);

        Task<string> GetServiceAMessageTest3Async(string id);
    }
}
