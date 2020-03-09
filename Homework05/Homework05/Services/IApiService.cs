using Homework05.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Homework05.Services
{
    public interface IApiService
    {
        [Get("/v3/top/anime/{page}")]
        Task<AnimeList> GetTopAnimes(int page);


        [Get("/v3/search/anime?q={anime}")]
        Task<AnimeList> SearchAnime(string anime);


        [Get("/v3/anime/{id}")]
        Task<Anime> SearchAnimeID(int id);
    }
}
