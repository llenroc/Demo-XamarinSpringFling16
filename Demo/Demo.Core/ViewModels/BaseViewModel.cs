using Demo.Core.Services;
using Demo.Core.Services.Message;
using Demo.Core.Services.Network;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace Demo.Core.ViewModels
{
    /// <summary>
    /// ViewModel Base que implementa métodos de navegación, y propiedades para mostrar loader y consumir servicios de la API.
    /// </summary>
    public class BaseViewModel : MvxViewModel
    {
        #region Members
        public IDataService DataService;
        public INetworkService NetworkService;
        public IMessageService MessageService;

        #endregion

        private bool isLoading;
        public virtual bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged();
            }
        }
        
        protected void ShowViewModel<TViewModel>(object parameters = null, bool clearbackstack = false) where TViewModel : MvxViewModel
        {
            if (clearbackstack)
            {
                var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "ClearBackFlag", "" } });

                ShowViewModel<TViewModel>(parameters, presentationBundle: presentationBundle);
                return;
            }

            // Normal start
            base.ShowViewModel<TViewModel>(parameters);
        }
    }
}
