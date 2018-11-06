using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
namespace Core.Helpers
{
    public class ApiHttpClient : IApiHttpClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpContext _context;
        public ApiHttpClient(IConfiguration configuration,IHttpContextAccessor context)
        {
            _configuration = configuration;
            _context = context.HttpContext;
        }

        public HttpClient GetOrgClient()
        {
            var client = new HttpClient();
            var url = _configuration.GetSection("OrgAPI").GetSection("url").Value;
            client.BaseAddress =new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
