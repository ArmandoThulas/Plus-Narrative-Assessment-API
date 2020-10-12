using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlusNarrativeAssessment.Data
{
    [Table("Game")]
    public class Game
    {
        [Key]
        public string gameId { get; set; }
        public string playerName { get; set; }
        public Guid playerId { get; set; }
        public Guid? opponentId { get; set; }
        public string opponentName { get; set; }
        public bool isHostCompleted { get; set; }
        public bool isOpponentCompleted { get; set; }

    }
}
