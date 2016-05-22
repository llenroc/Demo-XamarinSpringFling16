using Android.App;
using Android.Net;
using Demo.Core.Services.Network;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid;

namespace Demo.Droid.Services.Network
{
    public class DroidNetworkService : AbstractNetworkService
    {

        public DroidNetworkService()
        {
            NetworkConnectionBroadcastReceiver.OnChange = x => this.SetFromInfo(x, true);

            Mvx.CallbackWhenRegistered<IMvxAndroidGlobals>(x => {
                var manager = (ConnectivityManager)x.ApplicationContext.GetSystemService(Android.Content.Context.ConnectivityService);
                this.SetFromInfo(manager.ActiveNetworkInfo, false);
            });
        }

        private void SetFromInfo(NetworkInfo network, bool fireEvent)
        {
            this.SetStatus(
                network != null && network.IsConnected,
                (network != null && network.Type == ConnectivityType.Wifi),
                (network != null && network.Type == ConnectivityType.Mobile),
                fireEvent
            );
        }
    }
}