using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility.Interfaces
{
    public interface IApiCaller
    {
        Task CallApi(HttpMethod method, string endpoint, object content);
        Task<T> CallApi<T>(HttpMethod method, string endpoint);
    }
}
