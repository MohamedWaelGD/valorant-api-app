using Newtonsoft.Json.Linq;

namespace ValorantAPIApp.Services
{
    public class HttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
        }

        public async Task<ResponseAPI<string>> Request(string param, HttpMethod httpMethod)
        {
            var response = new ResponseAPI<string>();

            var URL = _configuration.GetSection("ValorantAPI").Value + param;
            var httpRequest = new HttpRequestMessage(httpMethod, URL);

            var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.SendAsync(httpRequest);

            if (!result.IsSuccessStatusCode)
            {
                response.Success = false;
                response.Message = "Error Happened.";
            }
            else
            {
                response.Data = await result.Content.ReadAsStringAsync();
            }

            return response;
        }

        public async Task<ResponseAPI<string>> RequestData(string param, HttpMethod httpMethod)
        {
            var response = new ResponseAPI<string>();

            var URL = _configuration.GetSection("ValorantAPI").Value + param;
            var httpRequest = new HttpRequestMessage(httpMethod, URL);

            var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.SendAsync(httpRequest);

            if (!result.IsSuccessStatusCode)
            {
                response.Success = false;
                response.Message = "Error Happened.";
            }
            else
            {
                response.Data = JObject.Parse(await result.Content.ReadAsStringAsync()).Value<string>("data");
            }

            return response;
        }
    }
}
