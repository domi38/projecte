using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projecte.Controllers
{
    using System.Web.Mvc;
    using Data.Entities;
    using Data.Services;
    using Models;
    using MongoDB.Bson;
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            var gameService = new GameService();
            var gameDetails = gameService.GetGamesDetails(100, 0);
            return View(gameDetails);
        }
        public ActionResult Reversi()
        {
            return View(new Player());
        }

    }
}
