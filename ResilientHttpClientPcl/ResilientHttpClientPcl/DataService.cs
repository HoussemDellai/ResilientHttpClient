using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ResilientHttpClientPcl
{
    public  class DataService
    {
        private readonly IHttpClient _httpClient;
        private const string Uri = "https://geoscan.azurewebsites.net/api/GetProductsTrigger";

        public DataService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetProductsAsync()
        {
            var json = await _httpClient.GetStringAsync(Uri);

            var products = JsonConvert.DeserializeObject<List<string>>(json);

            return products;
        }
    }
}
