using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using PlusNarrativeAssessment.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace PlusNarrativeAssessment.Business.MovieBusiness
{
    public class MovieBusiness
    {
        public List<MovieModel> GetMovies(bool includeMovieYear)
        {
            return GetContent(includeMovieYear);
        }

        private List<MovieModel> GetContent(bool includeMovieYear)
        {
            var model = new List<MovieModel>();
            var htmlNode = GetHtml(AppSettings.imdbUrl);
            var moviesRow = htmlNode.CssSelect("tbody.lister-list > tr");
            foreach (var item in moviesRow)
            {
                var titleColumn = item.CssSelect("td.titleColumn");
                var titleValue = titleColumn as HtmlNode[] ?? titleColumn.ToArray();
                var rankValue = titleValue.FirstOrDefault()?.InnerText.Split('.')[0];
                var yearValue = item.CssSelect("span.secondaryInfo").FirstOrDefault()?.InnerText;
                var imdbRating = item.CssSelect("td.imdbRating > strong").FirstOrDefault()?.InnerText;
                var imageAttributes = item.CssSelect("td.posterColumn > a > img").First().Attributes;
                var imageUrl = imageAttributes["src"].Value;
                if (rankValue == null || yearValue == null || !titleValue.Any())
                    continue;
                var movie = GetMovieDetails(rankValue, titleValue, yearValue, imdbRating, imageUrl, includeMovieYear);
                model.Add(movie);
            }

            return model;
        }

        private static HtmlNode GetHtml(string url)
        {
            var browser = new ScrapingBrowser();
            var webpage = browser.NavigateToPage(new Uri(url));
            return webpage.Html;
        }

        private static MovieModel GetMovieDetails(string rankValue, HtmlNode[] titleValue, string yearValue, string imdbRating, string imageUrl, bool includeMovieYear)
        {
            var movie = new MovieModel
            {
                rank = Convert.ToInt32(new string(rankValue.Where(char.IsDigit).ToArray())),
                title = titleValue.CssSelect("a").FirstOrDefault()?.InnerText,
                year = includeMovieYear ? Convert.ToInt32(new string(yearValue.Where(char.IsDigit).ToArray())) : 0,
                rating = imdbRating,
                imageUrl = imageUrl
            };
            return movie;
        }

        public List<MovieModel> GetRandomMovies()
        {
            var random = new Random();
            var movies = GetContent(false);
            var randomMovies = Enumerable.Range(0, AppSettings.gameRound).Select(x => movies[random.Next(0, movies.Count - 1)]).ToList();
            return randomMovies;
        }
    }
}
