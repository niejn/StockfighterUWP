using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HttpMethod = System.Net.Http.HttpMethod;
using HttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace StockfighterUWP.Requests
{
    class RequestHelper
    {
        public const string ApiUrl = "https://api.stockfighter.io/ob/api/";
        public const string ApiKey = "4f7c537ce4b923b340a285c8c8b3431275934937";

        public static string GetApiHeartbeatRequest()
        {
            return $"{ApiUrl}heartbeat";
        }

        public static string GetStocksInVenueRequest(string venue)
        {
            return $"{ApiUrl}venues/{venue}/stocks";
        }

        public static string GetStockOrderbook(string venue, string stock)
        {
            return $"{ApiUrl}venues/{venue}/stocks/{stock}";
        }

        public static HttpRequestMessage Order(string account, string venue, string stock, int price, int qty, string direction, string orderType)
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("X-Starfighter-Authorization", ApiKey);
            request.RequestUri = new Uri($"{ApiUrl}venues/{venue}/stocks/{stock}/orders");
            var jsonContent = new Dictionary<string, object>
            {
                ["account"] = account,
                ["venue"] = venue,
                ["stock"] = stock,
                ["qty"] = qty,
                ["direction"] = direction,
                ["orderType"] = orderType
            };
            if (price != 0)
            {
                jsonContent["price"] = price;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(jsonContent));
            request.Method = HttpMethod.Post;
            return request;
        }
    }
}
