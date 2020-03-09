using Homework05.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework05.Services
{
    public class ApiService : IApiService
    {
        private readonly IApiService _apiService;

        public ApiService()
        {
            _apiService = RestService.For<IApiService>(Config.urlApi);
        }

        public async Task<AnimeList> GetTopAnimes(int page)
        {
            return await _apiService.GetTopAnimes(page);
        }

        public async Task<AnimeList> SearchAnime(string anime)
        {
            return await _apiService.SearchAnime(anime);
        }

        public async Task<Anime> SearchAnimeID(int id)
        {
            return await _apiService.SearchAnimeID(id);
        }
    }
}
