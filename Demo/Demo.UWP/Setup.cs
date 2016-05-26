using Demo.Core.Services.Message;
using Demo.Core.Services.Network;
using Demo.UWP.Services.Message;
using Demo.UWP.Services.Network;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Plugins;
using MvvmCross.WindowsUWP.Platform;
using MvvmCross.WindowsUWP.Views;
using Windows.UI.Xaml.Controls;

namespace Demo.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            Mvx.RegisterType<IMessageService, MessageService>();
            
            Mvx.RegisterSingleton<INetworkService>(() => new WinUWPNetworkService());
            base.LoadPlugins(pluginManager);
        }
       
        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            return new MvxWindowsMultiRegionViewPresenter(rootFrame);
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
        }
    }
}
