using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlusNarrativeAssessment.Business.MovieBusiness;
using PlusNarrativeAssessment.Models;

namespace PlusNarrativeAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MovieModel> Get()
        {            
            var business = new MovieBusiness();
            return business.GetMovies(false).ToArray();
        }
    }
}