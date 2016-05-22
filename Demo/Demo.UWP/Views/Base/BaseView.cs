using Demo.UWP.Converters;
using MvvmCross.WindowsUWP.Views;
using System;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Demo.UWP.Views
{
    public class BaseView : MvxWindowsPage
    {
        public Frame AppFrame { get { return (Frame)this.WrappedFrame.UnderlyingControl; } }

        #region Lifecycle

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SystemNavigationManager.GetForCurrentView().BackRequested -= SystemNavigationManager_BackRequested;

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;


            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                DisplayProperties.AutoRotationPreferences = DisplayOrientations.Portrait;

                if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    var statusbar = StatusBar.GetForCurrentView();
                    await statusbar.ShowAsync();
                    statusbar.BackgroundColor = ColorConverter.GetColor("#FF000000");
                    statusbar.BackgroundOpacity = 1;
                    statusbar.ForegroundColor = ColorConverter.GetColor("#FFFFFFFF");
                }
            }
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled && this.AppFrame.CanGoBack)
            {
                e.Handled = true;
                this.AppFrame.GoBack();
            }
        }

        #endregion
    }
}
