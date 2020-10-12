using System;
using System.Collections.Generic;
using System.Text;

namespace PlusNarrativeAssessment.Models
{
    public class ResultModel
    {
        public string gameId { get; set; }
        public string playerName { get; set; }
        public Guid playerId { get; set; }
        public List<MovieModel> playerMovies { get; set; }
        public int playerResult { get; set; }
    }
}
