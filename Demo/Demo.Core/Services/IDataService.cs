using Demo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    // Expone métodos para consumir la API de Last.fm
    public interface IDataService
    {
        Task<IReadOnlyCollection<MTrack>> GetTopTracksList(string country="Mexico");
        Task<IReadOnlyCollection<MArtist>> GetTopArtistsList(string country = "Mexico");

        Task<MAlbum> GetAlbumInfo(string album, string artist);
        Task<MArtist> GetArtistInfo(string artist);
        Task<MTrack> GetTrackInfo(string track, string artist);

        Task<IReadOnlyCollection<MAlbum>> SearchAlbum(string album);
        Task<IReadOnlyCollection<MArtist>> SearchArtist(string artist);
        Task<IReadOnlyCollection<MTrack>> SearchTrack(string track, string artist);
    }
}
