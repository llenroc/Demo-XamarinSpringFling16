using Demo.Core.Services.Network;
using System.Linq;
using Windows.Networking.Connectivity;

namespace Demo.UWP.Services.Network
{
    public class WinUWPNetworkService : AbstractNetworkService
    {
        public WinUWPNetworkService()
        {
            NetworkInformation.NetworkStatusChanged += this.OnNetworkStatusChanged;
            this.OnNetworkStatusChanged(null);
        }
        
        private void OnNetworkStatusChanged(object sender)
        {
            var inetprof = NetworkInformation.GetInternetConnectionProfile();
            var profiles = NetworkInformation.GetConnectionProfiles();
            var inet = profiles.Any(x =>
                x.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess ||
                x.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.ConstrainedInternetAccess) || 
                (inetprof != null && (
                inetprof.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess ||
                inetprof.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.ConstrainedInternetAccess));

            var wifi = profiles.Any(x => x.IsWwanConnectionProfile);
            var mobile = profiles.Any(x => x.IsWwanConnectionProfile);
            this.SetStatus(inet, wifi, mobile, true);
        }
    }
}
