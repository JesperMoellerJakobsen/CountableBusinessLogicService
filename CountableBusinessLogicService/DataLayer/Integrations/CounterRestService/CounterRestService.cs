using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Config;
using Domain.Model.Entities;
using Newtonsoft.Json;

namespace Integrations.CounterRestService
{
    public class CounterRestService : ICounterRestService
    {
        private readonly string _serviceUrl;
        private readonly HttpClient _httpClient;

        public CounterRestService(CounterMicroserviceConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serviceUrl = configuration.ServiceUrl;
        }

        public async Task<ICounter> GetCounter()
        {
            var jsonResult = await _httpClient.GetStringAsync(_serviceUrl);
            return JsonConvert.DeserializeObject<Counter>(jsonResult);
        }

        public async Task<bool> TryIncrement(byte[] currentVersion)
        {
            return await Execute(currentVersion, PatchOptionType.Increment);
        }

        public async Task<bool> TryDecrement(byte[] currentVersion)
        {
            return await Execute(currentVersion, PatchOptionType.Decrement);
        }

        private async Task<bool> Execute(byte[] currentVersion, PatchOptionType type)
        {
            var patchArgs = new PatchArgs(currentVersion, type);
            var json = JsonConvert.SerializeObject(patchArgs);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync(_serviceUrl, content);
            return response.IsSuccessStatusCode;
        }
    }
}
