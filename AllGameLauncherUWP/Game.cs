using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AllGameLauncherUWP
{
    public class Game
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("evaluation")]
        public string Ocen { get; set; }

        [JsonProperty("date added")]
        public DateTime Add { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }


    }
}
