using System;
using System.Collections.Generic;
using System.Text;

namespace PlusNarrativeAssessment.Models
{
    public class GameModel
    {
        public string gameId { get; set; }
        public string playerName { get; set; }
        public Guid playerId { get; set; }
        public List<MovieModel> playerMovies { get; set; }
        public Guid? opponentId { get; set; }
        public string opponentName { get; set; }
        public List<MovieModel> opponentsMovies { get; set; }
        public bool isHostCompleted { get; set; }
        public bool isOpponentCompleted { get; set; }
    }
}
