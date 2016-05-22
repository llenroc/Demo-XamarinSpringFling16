using MvvmCross.Platform.Plugins;

namespace Demo.iOS.Bootstrap
{
    public class ConnectivityPluginBootstrap
        : MvxLoaderPluginBootstrapAction<Cheesebaron.MvxPlugins.Connectivity.PluginLoader, 
			Cheesebaron.MvxPlugins.Connectivity.Touch.Plugin> { }
}