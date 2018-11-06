using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Core.Helpers
{
    public interface IApiHttpClient
    {
        HttpClient GetOrgClient();
    }
}
