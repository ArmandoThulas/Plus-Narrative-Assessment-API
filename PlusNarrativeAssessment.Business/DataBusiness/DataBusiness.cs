using PlusNarrativeAssessment.Data;
using PlusNarrativeAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusNarrativeAssessment.Business.DataBusiness
{
    public class DataBusiness
    {
        private readonly DataContext context;

        public DataBusiness()
        {
            context = new DataContext();
        }
        public void AddGame(GameModel model)
        {
            var dbModel = MappingProfile.Mapper.Map<Game>(model);
            context.Games.Add(dbModel);
            context.SaveChanges();
        }

        public GameModel GetGameById(string id)
        {
            var result = context.Games.Find(id);
            if (result == null) return null;
            return MappingProfile.Mapper.Map<GameModel>(result);
        }

        public void UpdateGame(GameModel model)
        {
            var dbModel = MappingProfile.Mapper.Map<Game>(model);
            context.Games.Update(dbModel);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            var games = context.Games.ToList();
            games.ForEach(x => context.Games.Remove(x));
            var answers = context.PlayerAnswers.ToList();
            answers.ForEach(x => context.Remove(x));
            context.SaveChanges();
        }

        public void AddplayerAnswers(PlayerModel model)
        {
            var answers = new List<PlayerAnswer>();
            foreach (var item in model.playerMovies)
            {
                var dbModel = new PlayerAnswer
                {
                    gameId = model.gameId,
                    playerId = model.playerId,
                    movieRank = item.rank,
                    year = item.year
                };
                answers.Add(dbModel);
            }
            context.PlayerAnswers.AddRange(answers);
            context.SaveChanges();
        }

        public List<MovieModel> GetPlayerAnswers(PlayerModel model)
        {
            var result = context.PlayerAnswers.Where(x => x.playerId == model.playerId && x.gameId == model.gameId).ToList();
            var movies = new List<MovieModel>();
            result.ForEach(x =>
            {
                movies.Add(new MovieModel
                {
                    rank = x.movieRank,
                    year = x.year
                });
            });
            return movies;
        }
    }
}
