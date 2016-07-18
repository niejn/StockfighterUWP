using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Devices.Geolocation;
using System.Diagnostics;

namespace StockfighterUWP
{
    class TraderModel
    {
        private HttpClient _client;

        private const string ApiKey = "4f7c537ce4b923b340a285c8c8b3431275934937";

        private string _venue;

        private string _account;

        private const string ApiUrl = "https://api.stockfighter.io/ob/api/";

        public TraderModel(string venue = null, string account = null)
        {
            _client = new HttpClient();

            if (venue != null)
            {
                _venue = venue;
            }
            if (account != null)
            {
                _account = account;
            }
            
        }

        public async Task Init()
        {
            if (await ApiIsUp())
            {
                Debug.WriteLine("Stockfighter API is up.");
            }
        }

        private async Task<bool> ApiIsUp()
        {
            var response = await _client.GetAsync($"{ApiUrl}heartbeat");
            return false;
        }


    }
}
