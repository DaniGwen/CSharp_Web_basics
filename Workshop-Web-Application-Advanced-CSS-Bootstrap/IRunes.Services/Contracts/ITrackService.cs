using IRunes.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services.Contracts
{
    public interface ITrackService
    {
        Track GetTrackById(string id);
    }
}
