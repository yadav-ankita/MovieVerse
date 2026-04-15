using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieVerse.Models;

namespace MovieVerse.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAPICalls _apiCalls;

        private const string MovieType = "Movie";
        private const string SeasonType = "Season";
        private const string ApiNotFetchedView = "ApiNotFetched";
        private const string SearchMoviesView = "SearchMovies";
        private const string SearchSeasonView = "SearchSeason";
        private const string GetMovieView = "GetMovie";
        private const string GetSeasonView = "GetSeason";
        private const string PlaynowMovieView = "PlaynowMovie";
        private const string PlaynowSeasonView = "PlaynowSeason";
        private const string PlaySeasonListView = "PlaySeasonList";
        private const string PlaySeasonView = "PlaySeason";
        private const string PlayMovieView = "PlayMovie";

        public CollectionController(IAPICalls apiCalls, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiCalls = apiCalls;
        }
        public async Task<IActionResult> GetMovie()
        {
            try
            {
                var result = await _apiCalls.GetMovie();
                if (result.API_Fetched && result.Results != null)
                {
                    return View(result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }

        [Authorize]
        public async Task<IActionResult> GetMoviePage(int page)
        {
            if (page < 1)
                return View(ApiNotFetchedView);
            try
            {
                var result = await _apiCalls.GetMoviePage(page);
                if (result.API_Fetched && result.Results != null)
                {
                    return View(GetMovieView, result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }
        [Authorize]
        public async Task<IActionResult> PlaynowMovie(int playnow)
        {
            if (playnow < 1)
                return View(ApiNotFetchedView);
            try
            {
                var result = await _apiCalls.PlaynowMovie(playnow);
                if (result.API_Fetched)
                {
                    return View(PlaynowMovieView, result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Search(string search, string type = MovieType)
        {
            if (string.IsNullOrWhiteSpace(search) || string.IsNullOrWhiteSpace(type))
                return View(ApiNotFetchedView);
            return RedirectToAction(nameof(SearchPage), new { page = 1, type = type, search = search });
        }

        [Authorize]
        public async Task<IActionResult> SearchPage(int page, string type, string search)
        {
            if (page < 1 || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(search))
                return View(ApiNotFetchedView);
            try
            {
                if (type == MovieType)
                {
                    var result = await _apiCalls.SearchMoviePage(search, page);
                    if (result.API_Fetched)
                    {
                        return View(SearchMoviesView, result);
                    }
                    else
                    {
                        return View(ApiNotFetchedView);
                    }
                }
                else if (type == SeasonType)
                {
                    var result = await _apiCalls.SearchSeasonPage(search, page);
                    if (result.API_Fetched)
                    {
                        return View(SearchSeasonView, result);
                    }
                    else
                    {
                        return View(ApiNotFetchedView);
                    }
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }
        public async Task<IActionResult> GetSeason()
        {
            try
            {
                var result = await _apiCalls.GetSeason();
                if (result.API_Fetched)
                {
                    return View(result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }
        [Authorize]
        public async Task<IActionResult> GetSeasonPage(int page)
        {
            if (page < 1)
                return View(ApiNotFetchedView);
            try
            {
                var result = await _apiCalls.GetSeasonPage(page);
                if (result.API_Fetched)
                {
                    return View(GetSeasonView, result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }
        [Authorize]
        public async Task<IActionResult> PlaynowSeason(int playnow, string title)
        {
            if (playnow < 1 || string.IsNullOrWhiteSpace(title))
                return View(ApiNotFetchedView);
            try
            {
                var result = await _apiCalls.PlaynowSeason(playnow);
                if (result.API_Fetched)
                {
                    ViewBag.PlaynowTitle = title;
                    return View(PlaynowSeasonView, result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }

        [Authorize]
        public async Task<IActionResult> PlaySeasonList(int playnow)
        {
            if (playnow < 1)
                return View(ApiNotFetchedView);
            try
            {
                var result = await _apiCalls.GetSeasonDetails(playnow);
                if (result.API_Fetched)
                {
                    return View(PlaySeasonListView, result);
                }
                else
                {
                    return View(ApiNotFetchedView);
                }
            }
            catch
            {
                return View(ApiNotFetchedView);
            }
        }
        [Authorize]
        public IActionResult PlaySeason(string playnow)
        {
            if (string.IsNullOrWhiteSpace(playnow))
                return View(ApiNotFetchedView);
            return View(PlaySeasonView, playnow);
        }

        [Authorize]
        public IActionResult PlayMovie(int playnow)
        {
            if (playnow < 1)
                return View(ApiNotFetchedView);
            return View(PlayMovieView, playnow);
        }
    }
}
