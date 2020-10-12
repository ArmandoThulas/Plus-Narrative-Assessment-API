using System;
using System.Collections.Generic;
using System.Text;

namespace PlusNarrativeAssessment.Models
{
    public class MovieModel
    {
        public int rank { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string imageUrl { get; set; }
        public string rating { get; set; }
        public bool isCorrect { get; set; }
        public int correctYear { get; set; }
    }
}
