using Android.App;
using Demo.Core.Services.Message;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System;
using System.Threading.Tasks;

namespace Demo.Droid.Services.Message
{
    public class MessageService : IMessageService
    {
        protected Activity CurrentActivity
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        public void Confirm(string message, Action okClicked, string title = null, string okButton = "OK", string cancelButton = "Cancel")
        {
            Confirm(message, confirmed => {
                if (confirmed)
                    okClicked();
            },
            title, okButton, cancelButton);
        }

        public void Confirm(string message, Action<bool> answer, string title = null, string okButton = "OK", string cancelButton = "Cancel")
        {
            Application.SynchronizationContext.Post(ignored => {
                if (CurrentActivity == null) return;
                new AlertDialog.Builder(CurrentActivity)
                    .SetMessage(message)
                        .SetTitle(title)
                        .SetPositiveButton(okButton, delegate {
                            if (answer != null)
                                answer(true);
                        })
                        .SetNegativeButton(cancelButton, delegate {
                            if (answer != null)
                                answer(false);
                        })
                        .SetCancelable(false)
                        .Show();
            }, null);
        }

        public Task<bool> ConfirmAsync(string message, string title = "", string okButton = "OK", string cancelButton = "Cancel")
        {
            var tcs = new TaskCompletionSource<bool>();
            Confirm(message, tcs.SetResult, title, okButton, cancelButton);
            return tcs.Task;
        }
        
        public void Alert(string message, Action done = null, string title = "", string okButton = "OK")
        {
            Application.SynchronizationContext.Post(ignored => {
                if (CurrentActivity == null) return;
                new AlertDialog.Builder(CurrentActivity)
                    .SetMessage(message)
                        .SetTitle(title)
                        .SetPositiveButton(okButton, delegate {
                            if (done != null)
                                done();
                        })
                        .SetCancelable(false)
                        .Show();
            }, null);
        }

        public Task AlertAsync(string message, string title = "", string okButton = "OK")
        {
            var tcs = new TaskCompletionSource<object>();
            Alert(message, () => tcs.SetResult(null), title, okButton);
            return tcs.Task;
        }
    }
}