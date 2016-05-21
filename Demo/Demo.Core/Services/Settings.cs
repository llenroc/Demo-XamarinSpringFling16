namespace Demo.Core.Services
{
    /// <summary>
    /// Clase que define constantes de las URL's para consumir la API de Last.fm
    /// </summary>
    public class Settings
    {
        public static class Servers
        {
            public static string MusicUrlBase = "http://ws.audioscrobbler.com/2.0/?";
            public static string APP_KEY = "30593efcbd6bf8f79adbc9b44b7852a4";
           
            public static string TopTracksURL = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.GEOTOPTRACKS}";
            public static string TopArtistsURL = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.GEOTOPARTISTS}";

            public static string ArtistInfoUrl = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.ARTISTINFO}";
            public static string ArtistSearchUrl = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.ARTISTSEARCH}";

            public static string AlbumInfoUrl = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.ALBUMINFO}";
            public static string AlbumSearchUrl = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.ALBUMSEARCH}";

            public static string TrackInfoUrl = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.TRACKINFO}";
            public static string TrackSearchUrl = $"{MusicUrlBase}format=json&api_key={APP_KEY}&method={Endpoint.TRACKSEARCH}";
            
        }

        public static class Endpoint
        {
            public static string GEOTOPTRACKS = "geo.gettoptracks&country={0}";
            public static string GEOTOPARTISTS = "geo.gettopartists&country={0}";
            public static string ALBUMSEARCH = "album.search&album={0}";
            public static string ALBUMINFO = "album.getinfo&album={0}&artist={1}&lang=es";
            public static string ARTISTSEARCH = "artist.search&artist={0}";
            public static string ARTISTINFO = "artist.getinfo&artist={0}&lang=es";
            public static string TRACKSEARCH = "track.search&track={0}";
            public static string TRACKINFO = "track.getinfo&track={0}&artist={1}";
        }
        
    }
}
