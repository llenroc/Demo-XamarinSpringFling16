using Demo.Core.Services.Network;
using ModernHttpClient;
using MvvmCross.Localization;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;


namespace Demo.Core.Services.WebApi
{
    public class WebApiService : IWebApiService
    {

        #region Properties
        /// <summary>
        /// Interfaz de acceso a la red
        /// </summary>
        private INetworkService NetworkService { get; set; }
        

        /// <summary>
        /// Interfaz de acceso al TextSource
        /// </summary>
        public IMvxLanguageBinder TextSource
        {
            get
            {
                return new MvxLanguageBinder("MusicApp", "CommonStrings");
            }
        }

        #endregion

        public WebApiService(INetworkService networkService)
        {
            NetworkService = networkService;
        }


        /// <summary>
        /// Permite establcer el Client para las peticiones y respuestas HTTP.
        /// </summary>
        /// <returns>Cliente HTTP.</returns>
        private HttpClient Client()
        {
            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.Timeout = new TimeSpan(0, 0, 15);
            
            return httpClient;
        }

        /// <summary>
        /// Permite serializar un objeto a JSON.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a serializar.</typeparam>
        /// <param name="payload">Objeto a serializar.</param>
        /// <returns>Una cadena serializada en formato Json.</returns>
        public string Serialize<T>(T payload)
        {
            return JsonConvert.SerializeObject(payload);
        }

        /// <summary>
        /// Permite deserializar un objeto de un JSON.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a deserializar.</typeparam>
        /// <param name="json">Cadena que contien el Json.</param>
        /// <returns>El tipo de objeto deserializado.</returns>
        public T Deserialize<T>(string json)
        {
            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<T>(json);
            else
                return default(T);
        }

        /// <summary>
        /// Permite obtener la respuesta de una petición GET como string.
        /// </summary>
        /// <param name="uri">La URI de la petición.</param>
        /// <returns>Una cadena con el contenido de la respuesta.</returns>
        public async Task<string> GetAsync(Uri uri)
        {
            if (!NetworkService.IsConnected)
                return null;
            // throw new HttpRequestException(TextSource.GetText(nameof(Settings.CommonText.WebServiceNoConnection)), null);

            using (HttpClient http = new HttpClient(new NativeMessageHandler()))
            {
                try
                {
                    HttpResponseMessage message = await http.GetAsync(uri);
                    //if (message.StatusCode == HttpStatusCode.NotFound)
                    //{
                    //    if (NetworkService.IsConnected)
                    //        return null;
                    //    //throw new HttpRequestException(TextSource.GetText(nameof(Settings.CommonText.WebServiceNoConnection)));
                    //    else
                    //        return null;
                    //    //throw new HttpRequestException(TextSource.GetText(nameof(Settings.CommonText.WebServiceNotFoundConnection)));
                    //}
                    if (message.IsSuccessStatusCode)
                    {
                        return await message.Content.ReadAsStringAsync();
                    }

                    return null;

                }
                catch (TaskCanceledException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }

            }
        }        
    }
}
