using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.BZip2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace DotaReplayConsole
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        private static readonly NameValueCollection settings = ConfigurationManager.AppSettings;
        private static readonly FileSystemWatcher watcher = new FileSystemWatcher();
        public static Process dota;
        public static Process obs;
        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World! Starting Dota 2...");
            Console.Write("Enter Match ID: ");
            string input = Console.ReadLine();
            int matchId;

            while (!int.TryParse(input, out matchId))
            {
                Console.Write("Enter Match ID: ");
                input = Console.ReadLine();
            }

            InitializeWatcher();

            Replay replay = await OpenDotaApi.GetReplay(matchId);
            Util.DownloadAndUnzipReplay(replay);

            //get match details and store hero/player_slot info in map
            Dictionary<string, int> heroesMap = await GetMatchDetails(matchId);

            int matchDurationInSeconds = await GetMatchLength(matchId);

            //ask user for input
            Console.WriteLine("Please enter the hero you want to view as: ");
            var userInput = Console.ReadLine();
            int playerSlot;
            bool success = heroesMap.TryGetValue(userInput, out playerSlot);
            
            //handling dire heroes which start with slot #128
            if (playerSlot >= 128)
            {
                playerSlot -= 122;
            }
            System.Diagnostics.Debug.WriteLine("player slot is: " + playerSlot);

            //launch dota
            await LaunchDota();
            if (dota != null)
            {
                await StartReplay(playerSlot);

            }
            await StartObs();
            await WatchObs(matchDurationSeconds);
            await StopObs();

            Console.ReadLine();
        }

        public static void InitializeWatcher()
        {
            //watcher.Filter = ".dem";
            watcher.Path = replayFolder;
            watcher.Created += new FileSystemEventHandler(onAdd);
            watcher.EnableRaisingEvents = true;
        }

        private static void onAdd(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("detected file change");
        }
        public static async Task LaunchDota()
        {
            Process.Start("C:\\Program Files (x86)\\Steam\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe");

            dota = Process.GetProcessesByName("dota2").FirstOrDefault();

        }
        public static async Task StartReplay(int playerSlot)
        {
            IntPtr h = dota.MainWindowHandle;
            SetForegroundWindow(h);
            //Thread.Sleep(10000);
            await Task.Delay(10000);
            Console.WriteLine("sending input..");
            System.Diagnostics.Debug.WriteLine("sending inputs...");

            var ahk = new AutoHotkey.Interop.AutoHotkeyEngine();
            ahk.LoadFile("../../test.ahk");
            ahk.ExecFunction("TestSend");
            ahk.ExecFunction("WatchThisReplay", matchID.ToString(), playerSlot.ToString());
        }

        public static async Task StartObs()
        {
            System.Diagnostics.Debug.WriteLine("inside StartObs");
            var obsStream = Process.GetProcessesByName("obs64");
            if (obsStream.Length == 0)
            {
                Process.Start("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\OBS Studio\\OBS Studio (64bit)");
            }

            obs = Process.GetProcessesByName("obs64").FirstOrDefault();

            IntPtr h = obs.MainWindowHandle;
            SetForegroundWindow(h);

            await Task.Delay(8000);
            System.Diagnostics.Debug.WriteLine("sending OBS inputs...");
            Debug.WriteLine("Starting stream..at: " + DateTime.Now);
            var ahk = new AutoHotkey.Interop.AutoHotkeyEngine();
            ahk.LoadFile("../../test.ahk");
            ahk.ExecFunction("StartStream");


        }

        public static async Task WatchObs(int durationInSeconds)
        {
            await Task.Delay(durationInSeconds * 1000);
        }

        public static async Task StopObs()
        {
            Debug.WriteLine("Stopping stream..at: " + DateTime.Now);
            obs = Process.GetProcessesByName("obs64").FirstOrDefault();

            IntPtr h = obs.MainWindowHandle;
            SetForegroundWindow(h);
            var ahk = new AutoHotkey.Interop.AutoHotkeyEngine();
            ahk.LoadFile("../../test.ahk");
            ahk.ExecFunction("StopStream");
        }

        public static string GetHeroNameFromId(int id)
        {
            JArray heroesList;
            using (StreamReader r = File.OpenText("hero_names.json"))
            {
                using (JsonTextReader reader = new JsonTextReader(r))
                {
                    heroesList = (JArray)JToken.ReadFrom(reader);
                }
            }

            foreach (var hero in heroesList)
            {
                if ((Convert.ToInt32(hero["id"])) == id)
                {
                    return hero["localized_name"].ToString();
                }
            }
            //System.Diagnostics.Debug.Write("Hero name is: " + heroesList[0]["localized_name"]);

            return "error";
        }

        public static async Task<Dictionary<string, int>> GetMatchDetails(int matchId)
        {
            //Get replay stats from opendota api
            var response = await client.GetAsync($"{settings["openDotaApi"]}/matches/{matchId}");
            var jsonString = await response.Content.ReadAsStringAsync();

            //parse json string to obj
            JObject obj = JObject.Parse(jsonString); //dynamic obj doesn't work here, see: https://stackoverflow.com/questions/39468096/how-can-i-parse-json-string-from-httpclient

            //store player info in map
            Dictionary<string, int> heroesMap = new Dictionary<string, int>();
            foreach (var player in obj["players"])
            {
                string heroName = GetHeroNameFromId(Convert.ToInt32(player["hero_id"]));
                System.Diagnostics.Debug.WriteLine("player has slot: " + player["player_slot"] + " and is playing: " + heroName);
                heroesMap.Add(heroName, Convert.ToInt32(player["player_slot"]));
            }

            return heroesMap;
        }

        public static async Task<int> GetMatchLength(long matchID)
        {
            //Get replay stats from opendota api
            var response = await client.GetAsync("https://api.opendota.com/api/matches/" + matchID);
            var jsonString = await response.Content.ReadAsStringAsync();

            //parse json string to obj
            JObject obj = JObject.Parse(jsonString); 
            Debug.WriteLine("duration is: " + obj["duration"].ToString());

            return (obj["duration"].ToObject<Int32>() + 100); //add 100 seconds to take care of the setup time 

        }
    }
}
