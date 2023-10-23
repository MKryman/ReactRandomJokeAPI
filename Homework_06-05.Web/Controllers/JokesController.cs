using Homework_06_05.Data;
using Homework_06_05.Web.ViewModels;
using Homeworok_06_05.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Homework_06_05.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly string _connectionString;

        [ActivatorUtilitiesConstructor]
        public JokesController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("alljokes")]
        public List<Joke> GetAllJokes()
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetJokes();
        }

        [HttpGet]
        [Route("getjoke")]
        public Joke GetJoke()
        {
            var client = new HttpClient();
            var json = client.GetStringAsync("https://jokesapi.lit-projects.com/jokes/random").Result;

            var repo = new JokeRepository(_connectionString);

            var joke = JsonSerializer.Deserialize<Joke>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return repo.AddJoke(joke);
        }

        [HttpGet]
        [Route("getlikes")]
        public GetLikesCountViewModel GetLikesForJoke(int id)
       {
            var repo = new JokeRepository(_connectionString);
            var likes = repo.GetLikesForJoke(id, true);
            var dislikes = repo.GetLikesForJoke(id, false);
            return new GetLikesCountViewModel
            {
                Likes = likes,
                DisLikes = dislikes
            };
        }

        [HttpPost]
        //[Authorize]
        [Route("jokereaction")]
        public void LikeDislikeJoke(LikeViewModel vm)
        {
            var repo = new JokeRepository(_connectionString);
            if (!User.Identity.IsAuthenticated)
            {
                return;
            }
            repo.LikeDislikeJoke(vm.JokeId, vm.UserId, vm.Like);
        }


    }
}
