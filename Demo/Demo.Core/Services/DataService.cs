using Demo.Core.Models;
using Demo.Core.Services.WebApi;
using MvvmCross.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    // Implementa los métodos para consumir la API de Last.fm
    public class DataService : IDataService
    {
        #region Properties
        /// <summary>
        /// Interfaz para el acceso al WebApiService
        /// </summary>
        public IWebApiService WebApiService { get; set; }
        
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor con base a MvvmCross y la inyección de dependencias
        /// </summary>
        /// <param name="webApiService">WebApiService para inyeccion de dependencias</param>
        public DataService(IWebApiService webApiService)
        {
            WebApiService = webApiService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Obtiene una lista de artistas más escuchados de un país específico (Por default México).
        /// </summary>
        /// <param name="country">Nombre del país.</param>
        /// <returns>Una lista de artistas relacionados al criterio de búsqueda.</returns>
        public async Task<IReadOnlyCollection<MArtist>> GetTopArtistsList(string country = "Mexico")
        {
            ObservableCollection<MArtist> result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.TopArtistsURL, country), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new ObservableCollection<MArtist>();
                        var artistInfo = JObject.Parse(response).SelectToken("topartists").SelectToken("artist").ToString();
                        result = WebApiService.Deserialize<ObservableCollection<MArtist>>(artistInfo);
                        foreach (var item in result)
                            item.Image = item.Images[4].Url; //Obtenemos la imagen más grande
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene una lista de canciones más escuchadas de un país específico (Por default México).
        /// </summary>
        /// <param name="country">Nombre del país.</param>
        /// <returns>Una lista de canciones relacionados al criterio de búsqueda.</returns>
        public async Task<IReadOnlyCollection<MTrack>> GetTopTracksList(string country = "Mexico")
        {
            ObservableCollection<MTrack> result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.TopTracksURL, country), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new ObservableCollection<MTrack>();
                        var artistInfo = JObject.Parse(response).SelectToken("tracks").SelectToken("track").ToString();
                        result = WebApiService.Deserialize<ObservableCollection<MTrack>>(artistInfo);

                        foreach (var item in result)
                            item.Image = item.Images[3].Url; // Obtenemos la imagen más grande

                        foreach (var item in result)
                            item.ArtistName = item.Artist.Name;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene la descripción de un álbum específico.
        /// </summary>
        /// <param name="album">Nombre del álbum.</param>
        /// <param name="artist">Nombre del artista.</param>
        /// <returns>Una descripción (si existe) del álbum buscado.</returns>
        public async Task<MAlbum>GetAlbumInfo(string album, string artist)
        {
            MAlbum result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.AlbumInfoUrl, album, artist), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new MAlbum();
                        JObject jresult = null;
                        jresult = JObject.Parse(response);
                        result.Tracks = new List<MTrack>();

                        foreach (JToken item in jresult.SelectToken("album").SelectToken("tracks").SelectToken("track").Children())
                            result.Tracks.Add(item.ToObject<MTrack>());
                        
                        result.Name = jresult.SelectToken("album").SelectToken("name").Value<string>();
                        result.Artist = jresult.SelectToken("album").SelectToken("artist").Value<string>();
                        result.Images = new List<MImage>(jresult.SelectToken("album").SelectToken("image").ToObject<List<MImage>>());
                        result.Image = result.Images[3].Url;

                        if (jresult.SelectToken("album").SelectToken("wiki") != null)
                            result.Wiki = jresult.SelectToken("album").SelectToken("wiki").ToObject<MBiography>();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Realiza búsqueda de un álbum específico.
        /// </summary>
        /// <param name="album">Nombre del álbum</param>
        /// <returns>Una lista de álbumes relacionados al criterio de búsqueda.</returns>
        public async Task<IReadOnlyCollection<MAlbum>> SearchAlbum(string album)
        {
            ObservableCollection<MAlbum> result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.AlbumSearchUrl, album), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new ObservableCollection<MAlbum>();
                        var artistInfo = JObject.Parse(response).SelectToken("results").SelectToken("albummatches").SelectToken("album").ToString();
                        result = WebApiService.Deserialize<ObservableCollection<MAlbum>>(artistInfo);

                        foreach (var item in result)
                            item.Image = item.Images[3].Url; //Obtenemos la imagen más grande
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene la descripción de un artista específico.
        /// </summary>
        /// <param name="artist">Nombre del artista.</param>
        /// <returns>Una descripción (si existe) del artista buscado.</returns>
        public async Task<MArtist> GetArtistInfo(string artist)
        {
            MArtist result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.ArtistInfoUrl, artist), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new MArtist();
                        var artistInfo = JObject.Parse(response).SelectToken("artist").ToString();
                        result = WebApiService.Deserialize<MArtist>(artistInfo);

                        result.Image = result.Images[3].Url; //Obtenemos la imagen más grande
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Realiza búsqueda de un artista específico.
        /// </summary>
        /// <param name="artist"></param>
        /// <returns>Una lista de artistas relacionados al criterio de búsqueda.</returns>
        public async Task<IReadOnlyCollection<MArtist>> SearchArtist(string artist)
        {
            ObservableCollection<MArtist> result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.ArtistSearchUrl, artist), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new ObservableCollection<MArtist>();
                        var artistInfo = JObject.Parse(response).SelectToken("results").SelectToken("artistmatches").SelectToken("artist").ToString();
                        result = WebApiService.Deserialize<ObservableCollection<MArtist>>(artistInfo);

                        foreach (var item in result)
                            item.Image = item.Images[3].Url; //Obtenemos la imagen más grande.
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene la descripción de una canción específica.
        /// </summary>
        /// <param name="track">Nombre de la canción.</param>
        /// <param name="artist">Nombre del artista o grupo.</param>
        /// <returns>Una descripción (si existe) de la canción buscada.</returns>
        public async Task<MTrack> GetTrackInfo(string track, string artist)
        {
            MTrack result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.TrackInfoUrl, track, artist), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new MTrack();
                        var artistInfo = JObject.Parse(response).SelectToken("track").ToString();
                        result = WebApiService.Deserialize<MTrack>(artistInfo);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Realiza búsqueda de una canción específica.
        /// </summary>
        /// <param name="track">Nombre de la canción.</param>
        /// <param name="artist">Nombre del artista.</param>
        /// <returns>Una lista de canciones relacionadas al criterio de búsqueda.</returns>
        public async Task<IReadOnlyCollection<MTrack>> SearchTrack(string track, string artist)
        {
            ObservableCollection<MTrack> result = null;

            string response = await WebApiService.GetAsync(new Uri(string.Format(Settings.Servers.TrackSearchUrl, track, artist), UriKind.Absolute));

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    if (!response.Contains("error"))
                    {
                        result = new ObservableCollection<MTrack>();
                        JObject jresult = null;

                        jresult = JObject.Parse(response);

                        foreach (JToken item in jresult.SelectToken("results").SelectToken("trackmatches").SelectToken("track").Children())
                        {
                            List<MImage> imagesResult = item.SelectToken("image").ToObject<List<MImage>>();
                            result.Add(new MTrack()
                            {
                                Name = item.SelectToken("name").Value<string>(),
                                ArtistName = item.SelectToken("artist").Value<string>(),
                                Url = item.SelectToken("url").Value<string>(),
                                Image = imagesResult.FirstOrDefault(x => x.Size == "extralarge").Url //Obtenemos la imagen más grande
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }

        #endregion
    }
}
