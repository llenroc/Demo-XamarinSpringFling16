using Foundation;
using Demo.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;

namespace Demo.iOS.Views.Base
{
	[Register("ShellView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
	public class MainView : BaseViewController<ShellViewModel>
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.ShowMenu();
		}
	}
}

