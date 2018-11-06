using System;
using System.Collections.Generic;
using System.Text;
using Core.Helpers;

namespace Services
{
    public class BaseService
    {
        protected readonly ApiHttpClient apiClient;
        public BaseService(ApiHttpClient client)
        {
            apiClient = client;
        }
    }
}
