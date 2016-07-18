using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Devices.Geolocation;
using System.Diagnostics;
using Newtonsoft.Json;

namespace StockfighterUWP
{
    class TraderModel
    {
        private HttpClient _client;

        private const string ApiKey = "4f7c537ce4b923b340a285c8c8b3431275934937";

        private string _venue;

        private string _account;

        private const string ApiUrl = "https://api.stockfighter.io/ob/api/";

        public string LastApiUrl;

        public TraderModel()
        {
            _client = new HttpClient();

            
        }

        public async Task Init(string venue, string account)
        {
            if (venue != null)
            {
                _venue = venue;
            }
            if (account != null)
            {
                _account = account;
            }

            if (await ApiIsUp())
            {
                Debug.WriteLine("Stockfighter API is up.");
            }
        }

        private async Task<bool> ApiIsUp()
        {
            var response = await Get($"{ApiUrl}heartbeat");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var parsedResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ToString());
            return (parsedResponse["ok"] == "true");
        }

        private async Task<HttpResponseMessage> Get (string request)
        {
            LastApiUrl = request;
            return await _client.GetAsync(request);
        }

    }
}
