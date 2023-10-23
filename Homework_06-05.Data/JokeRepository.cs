
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Homework_06_05.Data
{
    public class JokeRepository
    {
        private readonly string _connectionString;

        public JokeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Joke> GetJokes()
        {
            var context = new JokeContext(_connectionString);
            return context.Jokes.Include(j => j.UserLikedJokes)
                .ToList();
        }

        public Joke AddJoke(Joke joke)
        {
            var context = new JokeContext(_connectionString);
            if (!JokeExists(joke))
            {
                context.Jokes.Add(joke);
                context.SaveChanges();
                return joke;
            }

            return GetJokeByOrigId(joke.OriginalId);

        }

        public bool JokeExists(Joke joke)
        {
            var context = new JokeContext(_connectionString);
            return context.Jokes.Any(j => j.OriginalId == joke.OriginalId);
        }

        public Joke GetJokeByOrigId(int origId)
        {
            var context = new JokeContext(_connectionString);
            return context.Jokes.FirstOrDefault(j => j.OriginalId == origId);
        }

        public Joke GetJokeById(int id)
        {
            var context = new JokeContext(_connectionString);
            return context.Jokes.FirstOrDefault(j => j.Id == id);
        }


        public void LikeDislikeJoke(int jokeId, int userId, bool like)
        {
            var context = new JokeContext(_connectionString);

            var alreadyLikedByUser = context.UserLikedJokes.Any(j => j.JokeId == jokeId && j.UserId == userId);

            var userLikedJoke = new UserLikedJoke
            {
                JokeId = jokeId,
                UserId = userId,
                Liked =  like
            };

            if (alreadyLikedByUser)
            {
                context.UserLikedJokes.Update(userLikedJoke);
            }
            else
            {
                context.UserLikedJokes.Add(userLikedJoke);
            }
            context.SaveChanges();
        }

        public int GetLikesForJoke(int id, bool status)
        {
            var context = new JokeContext(_connectionString);
            return context.UserLikedJokes.Where(ulj => ulj.JokeId == id)
                .GroupBy(ulj => ulj.Liked == status)
                .Count();

        }

    }
}
