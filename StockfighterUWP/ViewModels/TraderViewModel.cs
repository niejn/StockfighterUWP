using System.Threading.Tasks;
using StockfighterUWP.Models;

namespace StockfighterUWP.ViewModels
{
    public class TraderViewModel : NotificationBase<TraderModel>
    {
        public TraderViewModel(TraderModel model = null) : base(model)
        {
            This.SetViewModel(this);
        }

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

        public async Task InitModel(string venue, string account)
        {
            await This.Init(venue, account);
        }
    }
}
