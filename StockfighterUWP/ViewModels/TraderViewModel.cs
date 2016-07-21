using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using StockfighterUWP.Models;
using StockfighterUWP.Requests;
using StockfighterUWP.Utilities;

namespace StockfighterUWP.ViewModels
{
    public class TraderViewModel : NotificationBase<TraderModel>
    {
        private readonly HttpClient _client;

        private string _venue;

        private string _account;

        public string LastApiUrlString
        {
            get { return This.LastApiUrlString; }
            set { SetProperty(This.LastApiUrlString, value, () => This.LastApiUrlString = value); }
        }

        public string StatusBarString
        {
            get { return This.StatusBarString; }
            set { SetProperty(This.StatusBarString, value, () => This.StatusBarString = value); }
        }

        public string BigTextBlockString
        {
            get { return This.BigTextBlockString; }
            set { SetProperty(This.BigTextBlockString, value, () => This.BigTextBlockString = value); }
        }

        public TraderViewModel(TraderModel model = null) : base(model)
        {
            _client = new HttpClient();
        }

        public async Task Init(string venue, string account)
        {
            _venue = !venue.IsNullOrEmpty() ? venue : "TESTEX";
            if (!account.IsNullOrEmpty())
            {
                _account = account;
            }

            if (await ApiIsUp())
            {
                Debug.WriteLine("Stockfighter API is up.");
                StatusBarString = "Stockfighter API is up.";
            }
        }

        private async Task<bool> ApiIsUp()
        {
            try
            {
                var response = await Get(RequestHelper.GetApiHeartbeatRequest());
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                StatusBarString = "Http request error";
                return false;
            }
        }

        public async Task<string> GetStockInVenue(string venue = null)
        {
            if (venue == null)
            {
                venue = _venue;
            }
            var response = await Get(RequestHelper.GetStocksInVenueRequest(venue));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> Buy(string stock, int qty, string venue = null, int price = 0,
            string orderType = null)
        {
            if (venue == null)
            {
                venue = _venue;
            }
            if (price == 0)
            {
                orderType = "market";
            }
            if (orderType == null)
            {
                orderType = "limit";
            }
            var request = RequestHelper.Order(_account, venue, stock, price, qty, "buy", orderType);
            var response = await Send(request);
            return response;
        }


        private async Task<HttpResponseMessage> Get(string request)
        {
            //LastApiUrlString = request;
            //BigTextBlockString += request;
            var response = await _client.GetAsync(request);
            await LogResponse(response);
            return response;
        }

        private async Task<HttpResponseMessage> Send(HttpRequestMessage request)
        {
            var response = await _client.SendAsync(request);
            await LogResponse(response);
            return response;
        }

        private async Task LogResponse(HttpResponseMessage response)
        {
            BigTextBlockString += $"[{DateTime.Now.ToString("HH:mm:ss.ff")}]: ";
            BigTextBlockString += $"{response.RequestMessage.RequestUri.ToString().TrimEnd()}\n\n";
            BigTextBlockString += $"{(await response.Content.ReadAsStringAsync()).TrimEnd()}\n\n";
        }

    }
}
