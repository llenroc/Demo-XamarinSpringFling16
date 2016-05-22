using Android.App;
using Android.Content;
using Android.Net;
using System;

namespace Demo.Droid.Services.Network
{
    [BroadcastReceiver(Enabled = true, Label = "Network Status Receiver")]
    [IntentFilter(new string[] { "android.net.conn.CONNECTIVITY_CHANGE" })]
    public class NetworkConnectionBroadcastReceiver : BroadcastReceiver
    {
        internal static Action<NetworkInfo> OnChange { get; set; }

        public override void OnReceive(Context context, Intent intent)
        {
            /*if (intent.Extras == null || OnChange == null)
                return;

            var ni = intent.Extras.Get(ConnectivityManager.ExtraNetworkInfo) as NetworkInfo;
            if (ni == null)
                return;

            OnChange(ni);*/

            if (OnChange == null)
                return;

            ConnectivityManager connectionManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

            if (connectionManager == null)
                return;
          
            NetworkInfo networkInfo = connectionManager.ActiveNetworkInfo;
            if (networkInfo == null || !networkInfo.IsConnected)
                return;
          
            OnChange(networkInfo);
        }
    }
}