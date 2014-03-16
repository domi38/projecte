using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecte.Models
{
    using Data.Entities;

    public class PlayerGames
    {

        public Player Player { get; set; }
        public List<Game> AvailableGames { get; set; }
    }
}