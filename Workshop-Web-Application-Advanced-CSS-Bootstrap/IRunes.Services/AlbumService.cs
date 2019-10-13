﻿using IRunes.App.Data;
using IRunes.App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private RunesDbContext context;

        public AlbumService()
        {
            this.context = new RunesDbContext();
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
