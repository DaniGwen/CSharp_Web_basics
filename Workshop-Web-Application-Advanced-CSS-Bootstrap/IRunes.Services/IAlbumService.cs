﻿using IRunes.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services
{
    public interface IAlbumService
    {
        Album CreateAlbum(Album album);

        ICollection<Album> GetAllAlbums();

        Album GetAlbumById(string id);
    }
}
