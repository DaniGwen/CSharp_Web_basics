using IRunes.App.Models;
using System.Collections.Generic;

namespace IRunes.Services.Contracts
{
    public interface IAlbumService
    {
        Album CreateAlbum(Album album);

        ICollection<Album> GetAllAlbums();

        Album GetAlbumById(string id);
    }
}
