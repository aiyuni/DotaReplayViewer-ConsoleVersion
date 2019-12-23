using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public static async Task<Replay> GetReplay(int matchId)
        {
            var response = await client.GetAsync($"{openDotaApi}/replays?match_id={matchId}");
            var json = await response.Content.ReadAsStringAsync();
            JObject replayJson = JObject.Parse(json);
            Replay replay = replayJson.ToObject<Replay>();
            return replay;
        }

        public static async Task<MatchDetails> GetMatchDetails()
    }
}
