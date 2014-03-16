using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecte.Controllers
{
    using System.Web.Mvc;
    using Data.Entities;
    using Data.Services;
    using Models;
    using MongoDB.Bson;

    public class PlayerController : Controller
    {
        public ActionResult Addscore(string playerId, string gameId, string gameName)
        {
            var playerService = new PlayerService();
            var score= new Score{
                GameId = new ObjectId(gameId),
                GameName= gameName,
                ScoreDateTime = DateTime.Now,
                ScoreValue=new Random().Next(0,Int32.MaxValue)
            };
            playerService.AddScore(playerId, score);
            return RedirectToAction("Details", new {id=playerId});
        }
        public ActionResult Registre()
        {
            return View(new Player());
        }

       //JA NO FARA REFERENCIA DIRECTA A PLAYER(DATA.ENTITES.PLAYER)
        //INTERMIG fikem playerViewModel (playerVM)
        public ActionResult CreatePlayer(PlayerVM player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Creem un player playerDTO i assignem atributs
                    Player playerDTO = new Player();
                    playerDTO.Name = player.Name;
                    playerDTO.Gender = player.Gender;
                    playerDTO.Password = player.Password;

                    //Creem playerService k crearà el nou player
                    var playerService = new PlayerService();
                    playerService.Create(playerDTO);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Guardartablero(List<List<char>> tablero)
        {
            try
            {
                tablero[0][0] = 'c';

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetById(id);
            return View(player);
        }
        [HttpPost]
        public ActionResult Delete(Player player)
        {
            try
            {
                var playerService = new PlayerService();
                playerService.Delete(player.Id.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(string id)
        { 
                var playerService = new PlayerService();
                var player = playerService.GetById(id);

                return View(player);        
        }

        public ActionResult Edit(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetById(id);

            return View(player);
        }
        //PER CHEKEJAR SI ENTRAVA AL CONTROLLER
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //}

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var playerService = new PlayerService();
                    playerService.Update(player);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            var playerService = new PlayerService();
            var playerDetails = playerService.GetPlayersDetails(100, 0);
            return View(playerDetails);
        }
        public ActionResult PlayGames(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetById(id);
            var gameService = new GameService();
            var availableGames = gameService.GetGamesDetails(100, 0);

            var playerGames = new PlayerGames()
            {
                Player = player,
                AvailableGames = new List<Game>(availableGames)
            };
            return View(playerGames);
        }

        public JsonResult GetGender()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //var gender = Enum.GetValues(typeof(Gender));
            result.Data = Enum.GetNames(typeof(Gender));
            return result;
        }
    }
}