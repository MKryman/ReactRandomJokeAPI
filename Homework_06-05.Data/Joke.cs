using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Homework_06_05.Data
{
    public class Joke
    {
        public int Id { get; set; }

        [JsonPropertyName("jokeId")]
        public int OriginalId { get; set; }

        [JsonPropertyName("setup")]
        public string Riddle { get; set; }

        [JsonPropertyName("punchline")]
        public string Punchline { get; set; }

        
        public List<UserLikedJoke> UserLikedJokes { get; set; }
    }
}