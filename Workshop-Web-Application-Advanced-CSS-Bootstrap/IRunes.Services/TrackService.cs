using IRunes.App.Data;
using IRunes.App.Models;
using IRunes.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class TrackService : ITrackService
    {
        private RunesDbContext context;

        public TrackService()
        {
            this.context = new RunesDbContext();
        }

        public Track CreateTrack(Track track)
        {
            track = this.context.Tracks.Add(track).Entity;
            this.context.SaveChanges();

            return track;
        }

        public Track GetTrackById(string id)
        {
            var track = this.context.Tracks.SingleOrDefault(t => t.Id == id);

            return track;
        }
    }
}
