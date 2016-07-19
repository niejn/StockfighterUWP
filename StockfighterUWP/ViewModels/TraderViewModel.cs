using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using StockfighterUWP.Models;

namespace StockfighterUWP.ViewModels
{
    public class TraderViewModel : NotificationBase<TraderModel>
    {
        private readonly HttpClient _client;

        private const string ApiKey = "4f7c537ce4b923b340a285c8c8b3431275934937";

        private string _venue;

        private string _account;

        private const string ApiUrl = "https://api.stockfighter.io/ob/api/";

        public string LastApiUrlString
        {
            get { return This.LastApiUrl; }
            set { SetProperty(This.LastApiUrl, value, () => This.LastApiUrl = value); }
        }

        public string StatusBarString
        {
            get { return This.StatusBar; }
            set { SetProperty(This.StatusBar, value, () => This.StatusBar = value); }
        }

        public TraderViewModel(TraderModel model = null) : base(model)
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
                StatusBarString = "Stockfighter API is up.";
            }
        }

        private async Task<bool> ApiIsUp()
        {
            try
            {
                var response = await Get($"{ApiUrl}heartbeat");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                StatusBarString = "Http request error";
                return false;
            }
        }

        private async Task<HttpResponseMessage> Get(string request)
        {
            LastApiUrlString = request;
            return await _client.GetAsync(request);
        }
    }
}
