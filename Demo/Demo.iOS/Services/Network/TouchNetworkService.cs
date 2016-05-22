using System;
using System.Threading.Tasks;
using Demo.Core.Services.Network;

namespace Demo.iOS.Services.Network
{
	/// <summary>
	/// Clase para obtener el servicio de red en iOS.
	/// </summary>
	public class TouchNetworkService : AbstractNetworkService
	{

		/// <summary>
		/// Constructor inicial
		/// </summary>
		public TouchNetworkService()
		{
			this.SetInfo(false);
			Reachability.ReachabilityChanged += (s, e) => this.SetInfo(true);
		}

		/// <summary>
		/// Método para disparar un evento en base al estatus de la conexiòn de red actual.
		/// </summary>
		/// <param name="fireEvent">Si se piensa disparar el evento de Disponibilidad</param>
		private void SetInfo(bool fireEvent)
		{
			switch (Reachability.InternetConnectionStatus())
			{
				case NetworkStatus.NotReachable:
					this.SetStatus(false, false, false, fireEvent);
					break;

				case NetworkStatus.ReachableViaCarrierDataNetwork:
					this.SetStatus(true, false, true, fireEvent);
					break;

				case NetworkStatus.ReachableViaWiFiNetwork:
					this.SetStatus(true, false, true, fireEvent);
					break;
			}
		}
	}
}

