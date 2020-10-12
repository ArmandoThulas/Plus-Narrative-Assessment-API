using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlusNarrativeAssessment.Data
{
    [Table("PlayerAnswer")]
    public class PlayerAnswer
    {
        [Key]
        public int id { get; set; }
        public Guid playerId { get; set; }
        public string gameId { get; set; }
        public int movieRank { get; set; }
        public int year { get; set; }
    }
}
