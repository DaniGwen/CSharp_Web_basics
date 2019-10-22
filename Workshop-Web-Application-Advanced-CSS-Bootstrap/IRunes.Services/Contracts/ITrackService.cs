using IRunes.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.IRunes.Services.Contracts
{
    public interface ITrackService
    {
        Track GetTrackById(string id);
    }
}
