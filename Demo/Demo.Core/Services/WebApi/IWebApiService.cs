using System;
using System.Threading.Tasks;

namespace Demo.Core.Services.WebApi
{
    public interface IWebApiService
    {

        /// <summary>
        /// Permite serializar un objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a serializar.</typeparam>
        /// <param name="payload">Objeto a serializar.</param>
        /// <returns>Una cadena serializada en formato JSON.</returns>
        string Serialize<T>(T payload);

        /// <summary>
        /// Permite deserializar un objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a deserializar.</typeparam>
        /// <param name="json">Cadena que contiene el JSON.</param>
        /// <returns>El tipo de objeto deserializado.</returns>
        T Deserialize<T>(string json);

        /// <summary>
        /// Permite obtener el contenido del response como cadena.
        /// </summary>
        /// <param name="uri">La URI de la petición.</param>
        /// <returns>Una cadena con el contenido del response.</returns>
        Task<string> GetAsync(Uri uri);
        
    }
}
