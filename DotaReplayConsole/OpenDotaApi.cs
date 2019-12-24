using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotaReplayConsole
{
    class OpenDotaApi
    {
        private static readonly NameValueCollection settings = ConfigurationManager.AppSettings;
        private static readonly HttpClient client = new HttpClient();
        private static string openDotaApi = settings["openDotaApi"];
        private static List<Hero> heroesList;

        static OpenDotaApi()
        {
            using (StreamReader r = new StreamReader("hero_names.json"))
            {
                string json = r.ReadToEnd();
                heroesList = JsonConvert.DeserializeObject<List<Hero>>(json);
            }
        }

        public static async Task<Replay> GetReplay(long matchId)
        {
            var response = await client.GetAsync($"{openDotaApi}/replays?match_id={matchId}");
            var json = await response.Content.ReadAsStringAsync();
            JArray replaysJson = JArray.Parse(json);
            Replay replay = JsonConvert.DeserializeObject<Replay>(replaysJson[0].ToString());
            return replay;
        }

        public static async Task<Match> GetMatch(long matchId)
        {
            var response = await client.GetAsync($"{openDotaApi}/matches/{matchId}");
            var json = await response.Content.ReadAsStringAsync();
            Match match = JsonConvert.DeserializeObject<Match>(json);
            return match;
        }

        public static Dictionary<int, Hero> GetMatchHeroes(Match match)
        {
            Dictionary<int, Hero> heroes = new Dictionary<int, Hero>();
            foreach (var player in match.players)
            {
                Hero hero = GetHeroFromId(player.hero_id);
                heroes.Add(player.player_slot, hero);
            }
            return heroes;
        }

        public static Hero GetHeroFromId(int heroId)
        {          
            foreach (var hero in heroesList)
            {
                if (hero.id == heroId)
                {
                    return hero;
                }
            }
            return null;
        }
    }
}
