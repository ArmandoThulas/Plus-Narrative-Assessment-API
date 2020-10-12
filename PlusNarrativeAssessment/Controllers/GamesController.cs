using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PlusNarrativeAssessment.Business.DataBusiness;
using PlusNarrativeAssessment.Business.GameBusiness;
using PlusNarrativeAssessment.Models;

namespace PlusNarrativeAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<GameModel> Get()
        {
            var business = new GameBusiness();
            return business.GetHostedGames().ToArray();
        }

        [HttpPost]
        [Route("host")]
        public PlayerModel HostGame(PlayerModel model)
        {
            var business = new GameBusiness();
            var player = business.HostAGame(model);
            return player;
        }

        [HttpGet]
        [Route("{id}")]
        public GameModel GetGameById(string id)
        {           
            var model = new DataBusiness().GetGameById(id);
            return model;
        }

        [HttpPost]
        [Route("join")]
        public PlayerModel JoinGame(PlayerModel model)
        {
            var player = new GameBusiness().JoinGame(model);
            return player;
        }

        [HttpPost]
        [Route("player/answers")]
        public List<ResultModel> Submit(PlayerModel model)
        {
            var result = new GameBusiness().SubmitAnswers(model);
            return result;
        }

        [HttpPost]
        [Route("result")]
        public ResultModel Result(ResultModel model)
        {
            var result = new GameBusiness().GetFinalResult(model);
            return result;
        }
    }
}