using System;
using System.Collections.Generic;
using System.Text;

namespace PlusNarrativeAssessment.Models
{
    public class PlayerAnswerModel
    {
        public int id { get; set; }
        public Guid playerId { get; set; }
        public string gameId { get; set; }
        public int movieRank { get; set; }
        public int year { get; set; }
    }
}
