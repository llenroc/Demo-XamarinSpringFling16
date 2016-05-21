using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System;
using System.ComponentModel;

namespace Demo.Core.Services.Network
{
    /// <summary>
    /// Clase usada para verificar la conexión de red.
    /// Más información en https://github.com/aritchie/acrmvvmcross
    /// </summary>
    public abstract class AbstractNetworkService : INetworkService, INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Propiedad para verificar si está conectado a Internet
        /// </summary>
        private bool isConnected;
        public bool IsConnected
        {
            get { return this.isConnected; }
            private set
            {
                if (this.isConnected == value)
                    return;

                this.isConnected = value;
                this.OnPropertyChanged("IsConnected");
            }
        }

        /// <summary>
        /// Propiedad para
        /// </summary>
        private bool isWifi;
        public bool IsWifi
        {
            get { return this.isWifi; }
            private set
            {
                if (this.isWifi == value)
                    return;

                this.isWifi = value;
                this.OnPropertyChanged("IsWifi");
            }
        }

        /// <summary>
        /// Propiedad para verificar si la red es celular
        /// </summary>
        private bool isMobile;
        public bool IsMobile
        {
            get { return this.isMobile; }
            private set
            {
                if (this.isMobile == value)
                    return;

                this.isMobile = value;
                this.OnPropertyChanged("IsMobile");
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Método para notificar el cambio de red
        /// </summary>
        /// <param name="action">Acción a realizar después del cambio de red.</param>
        /// <returns>Token de MVVMCross</returns>
        public MvxSubscriptionToken Subscribe(Action<NetworkStatusChangedMessage> action)
        {
            return Mvx
                .Resolve<IMvxMessenger>()
                .Subscribe<NetworkStatusChangedMessage>(action);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Se usa para actualizar el estado de la red. 
        /// </summary>
        /// <param name="connected">Estatus de conexión de la red.</param>
        /// <param name="wifi">Si la conexión es WiFi</param>
        /// <param name="mobile">Si la conexión es red celular</param>
        /// <param name="fireEvent">Si es necesario notificar</param>
        protected void SetStatus(bool connected, bool wifi, bool mobile, bool fireEvent)
        {
            this.IsConnected = connected;
            this.IsWifi = wifi;
            this.IsMobile = mobile;

            if (fireEvent)
            {
                Mvx
                    .Resolve<IMvxMessenger>()
                    .Publish(new NetworkStatusChangedMessage(this));
            }
        }

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
