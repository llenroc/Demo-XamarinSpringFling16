using System;
using System.Threading.Tasks;
using Demo.Core.Services.Message;

namespace Demo.iOS.Services.Message
{
	public class TouchDialogService : IMessageService
	{
		public void Alert(string message, Action done = null, string title = "", string okButton = "OK")
		{
			
		}

		public Task AlertAsync(string message, string title = "", string okButton = "OK")
		{
			throw new NotImplementedException();
		}
	}
}

