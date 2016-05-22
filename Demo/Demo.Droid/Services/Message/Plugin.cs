using Demo.Core.Services.Message;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace Demo.Droid.Services.Message
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterType<IMessageService, MessageService>();
        }
    }
}