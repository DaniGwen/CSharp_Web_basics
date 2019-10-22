using Apps.IRunes.Services.Contracts;
using IRunes.App.Data;
using IRunes.App.Models;
using System.Linq;

namespace Apps.IRunes.Services
{
    public class TrackService : ITrackService
    {
        private RunesDbContext context;

        public TrackService()
        {
            this.context = new RunesDbContext();
        }

        public Track GetTrackById(string id)
        {
            var track = this.context.Tracks.SingleOrDefault(t => t.Id == id);

            return track;
        }
    }
}
