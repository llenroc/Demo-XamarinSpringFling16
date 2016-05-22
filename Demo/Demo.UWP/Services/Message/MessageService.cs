using Demo.Core.Services.Message;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Demo.UWP.Services.Message
{
    public class MessageService : IMessageService 
    {
        public void Alert(string message, Action done = null, string title = "", string okButton = "OK")
        {
            AlertAsync(message, title, okButton).ContinueWith((button) => { if (done != null) done(); });
        }

        public Task AlertAsync(string message, string title = "", string okButton = "OK")
        {
            var complete = new TaskCompletionSource<bool>();

            var dialog = new MessageDialog(message, title);
            dialog.Commands.Add(new UICommand(okButton, command => complete.TrySetResult(true)));
            dialog.ShowAsync().AsTask();
            return complete.Task;
        }
    }
}
