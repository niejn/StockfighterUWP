using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using StockfighterUWP.ViewModels;

namespace StockfighterUWP.Models
{
    /// <summary>
    /// MVVM model should be barebone only containing data/properties.
    /// These can be later write to I/O, disk if needed. Model in MVVM
    /// should only handle data-related tasks.
    /// </summary>
    public class TraderModel
    {
        public string LastApiUrlString { get; set; }

        public string StatusBarString { get; set; }
        public string BigTextBlockString { get; set; }
    }
}
