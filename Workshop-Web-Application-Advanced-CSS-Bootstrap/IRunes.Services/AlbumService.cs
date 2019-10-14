using IRunes.App.Data;
using IRunes.App.Models;
using IRunes.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private RunesDbContext context;

        public AlbumService()
        {
            this.context = new RunesDbContext();
        }

        public bool AddTrackToAlbum(string albumId, Track trackForDb)
        {
            var album = this.GetAlbumById(albumId);

            if (album == null)
            {
                return false;
            }

            album.Tracks.Add(trackForDb);
            album.Price = (album.Tracks.Select(track => track.Price).Sum() * 87) / 100;
            context.Update(album);
            context.SaveChanges();

            return true;
        }

        public Album CreateAlbum(Album album)
        {
            album = context.Albums.Add(album).Entity;
            context.SaveChanges();

            return album;
        }

        public Album GetAlbumById(string id)
        {
            return this.context.Albums
                .Include(a => a.Tracks)
                .SingleOrDefault(album => album.Id == id);
        }

        public ICollection<Album> GetAllAlbums()
        {
            return context.Albums.ToList();
        }
    }
}
