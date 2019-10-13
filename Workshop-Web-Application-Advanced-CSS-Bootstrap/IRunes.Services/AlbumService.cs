using System;
using System.Collections.Generic;
using System.Text;
using IRunes.App.Models;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private RunesDbContext context;

        public AlbumService()
        {

        }
        public Album CreateAlbum(Album album)
        {
            using (var context = new RunesDbContext())
            {
                string name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
                string cover = ((ISet<string>)this.Request.FormData["cover"]).FirstOrDefault();

                var album = new Album
                {
                    Cover = cover,
                    Name = name,
                    Price = 0M
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }
        }

        public Album GetAlbumById(string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Album> GetAllAlbums()
        {
            throw new NotImplementedException();
        }
    }
}
