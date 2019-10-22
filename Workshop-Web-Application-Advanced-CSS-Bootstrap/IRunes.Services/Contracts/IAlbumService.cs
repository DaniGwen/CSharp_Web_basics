using IRunes.App.Models;
using System.Collections.Generic;

namespace Apps.IRunes.Services.Contracts
{
    public interface IAlbumService
    {
        Album CreateAlbum(Album album);

        bool AddTrackToAlbum(string albumId, Track track);

        ICollection<Album> GetAllAlbums();

        Album GetAlbumById(string id);
    }
}
