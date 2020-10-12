using PlusNarrativeAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlusNarrativeAssessment.Business.GameBusiness
{
    public class GameBusiness
    {
        public List<GameModel> GetHostedGames()
        {
            return new List<GameModel>();
        }

        public PlayerModel HostAGame(PlayerModel model)
        {
            var game = new GameModel
            {
                playerName = model.playerName,
                playerId = Guid.NewGuid(),
                gameId = RandomString(),
                playerMovies = new MovieBusiness.MovieBusiness().GetRandomMovies()
            };
            new DataBusiness.DataBusiness().AddGame(game);
            return MappingProfile.Mapper.Map<PlayerModel>(game);
        }

        private static string RandomString()
        {
            var random = new Random();
            const string pool = AppSettings.alphabet;
            var chars = Enumerable.Range(0, AppSettings.gameIdLength)
                .Select(x => pool[random.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
        
        public PlayerModel JoinGame(PlayerModel model)
        {
            var game = new DataBusiness.DataBusiness().GetGameById(model.gameId);
            if (game == null) return null;
            game.opponentId = Guid.NewGuid();
            game.opponentName = model.playerName;
            game.opponentsMovies = new MovieBusiness.MovieBusiness().GetRandomMovies();
            new DataBusiness.DataBusiness().UpdateGame(game);
            return GetOpponentDetails(game);
        }

        public PlayerModel GetOpponentDetails(GameModel game)
        {
            var model = new PlayerModel
            {
                gameId = game.gameId,
                playerId = (Guid)game.opponentId,
                playerName = game.opponentName,
                playerMovies = game.opponentsMovies
            };
            return model;
        }

        public List<ResultModel> SubmitAnswers(PlayerModel model)
        {
            var game = new DataBusiness.DataBusiness().GetGameById(model.gameId);
            if (game == null) return null;
            var result = new List<ResultModel>();
            new DataBusiness.DataBusiness().AddplayerAnswers(model);
            var movies = new MovieBusiness.MovieBusiness().GetMovies(true);
            if (game.playerId.Equals(model.playerId))
                PlayerResult(model, game, result, movies);
            else
                OpponentResult(model, game, result, movies);
            return result;
        }

        private void PlayerResult(PlayerModel model, GameModel game, List<ResultModel> result, List<MovieModel> movies)
        {
            game.isHostCompleted = true;
            new DataBusiness.DataBusiness().UpdateGame(game);
            var playerResult = Marking(game, model, movies);
            result.Add(playerResult);
            if (game.isOpponentCompleted)
            {
                var player = MappingProfile.Mapper.Map<PlayerModel>(model);
                player.playerId = (Guid)game.opponentId;
                player.playerName = game.opponentName;
                player.playerMovies = new DataBusiness.DataBusiness().GetPlayerAnswers(player);
                var a = Marking(game, player, movies);
                result.Add(a);
            }
        }

        private void OpponentResult(PlayerModel model, GameModel game, List<ResultModel> result, List<MovieModel> movies)
        {
            game.isOpponentCompleted = true;
            new DataBusiness.DataBusiness().UpdateGame(game);
            var playerResult = Marking(game, model, movies);
            result.Add(playerResult);
            if (game.isHostCompleted)
            {
                var player = MappingProfile.Mapper.Map<PlayerModel>(model);
                player.playerId = game.playerId;
                player.playerName = game.playerName;
                player.playerMovies = new DataBusiness.DataBusiness().GetPlayerAnswers(player);
                var a = Marking(game, player, movies);
                result.Add(a);
            }
        }

        private ResultModel Marking(GameModel game, PlayerModel player, List<MovieModel> movies)
        {
            var result = new ResultModel();
            result.gameId = player.gameId;
            result.playerId = player.playerId;
            result.playerName = player.playerName;
            result.playerMovies = player.playerMovies;
            foreach (var item in result.playerMovies)
            {
                var answer = movies.FirstOrDefault(x => x.rank.Equals(item.rank));
                if (answer.year == item.year)
                {
                    result.playerResult += AppSettings.correctPoints;
                    item.correctYear = answer.year;
                    item.isCorrect = true;
                }
                else
                {
                    result.playerResult += AppSettings.incorrectPoints;
                    item.correctYear = answer.year;
                    item.isCorrect = false;
                }
            }
            return result;
        }

        public ResultModel GetFinalResult(ResultModel model)
        {
            var game = new DataBusiness.DataBusiness().GetGameById(model.gameId);
            if (game == null) return null;
            var movies = new MovieBusiness.MovieBusiness().GetMovies(true);
            if (game.playerId.Equals(model.playerId))
            {
                if (!game.isOpponentCompleted)
                    return null;
                else
                {
                    var player = MappingProfile.Mapper.Map<PlayerModel>(model);
                    player.playerId = (Guid)game.opponentId;
                    player.playerName = game.opponentName;
                    player.playerMovies = new DataBusiness.DataBusiness().GetPlayerAnswers(player);
                    return Marking(game, player, movies);
                }
            }
            else
            {
                if (!game.isHostCompleted)
                    return null;
                else
                {
                    var result = new List<ResultModel>();
                    var player = MappingProfile.Mapper.Map<PlayerModel>(model);
                    player.playerId = game.playerId;
                    player.playerName = game.playerName;
                    player.playerMovies = new DataBusiness.DataBusiness().GetPlayerAnswers(player);
                    return Marking(game, player, movies);
                }
            }
        }
    }
}
