using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using StockfighterUWP.ViewModels;

namespace StockfighterUWP.Models
{
    public class TraderModel
    {
        private readonly HttpClient _client;

        private const string ApiKey = "4f7c537ce4b923b340a285c8c8b3431275934937";

        private string _venue;

        private string _account;

        private const string ApiUrl = "https://api.stockfighter.io/ob/api/";

        public string LastApiUrl;

        public string StatusBar;

        private TraderViewModel _viewModel;

        public TraderModel()
        {
            _client = new HttpClient();
        }

        public void SetViewModel(TraderViewModel viewModel)
        {
            _viewModel = viewModel;
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
                _viewModel.StatusBarString = "Stockfighter API is up.";
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
                StatusBar = "Http request error";
                return false;
            }
        }

        private async Task<HttpResponseMessage> Get (string request)
        {
            //LastApiUrl = request;
            _viewModel.LastApiUrlString = request;
            return await _client.GetAsync(request);
        }

    }
}
