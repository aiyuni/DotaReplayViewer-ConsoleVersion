using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace DotaReplayConsole
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndParent);

        private static readonly NameValueCollection settings = ConfigurationManager.AppSettings;
        private static string replayFolder = settings["replayFolder"];

        private static readonly FileSystemWatcher watcher = new FileSystemWatcher();
        public static IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
        public static Process dota;
        public static Process obs;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World! Starting Dota 2...");
            Console.Write("Enter Match ID: ");
            string input = Console.ReadLine();
            long matchId;
            while (!long.TryParse(input, out matchId))
            {
                Console.Write("Enter Match ID: ");
                input = Console.ReadLine();
            }

            InitializeWatcher();

            Replay replay = await OpenDotaApi.GetReplay(matchId);
            Match match = await OpenDotaApi.GetMatch(matchId);
            //Util.DownloadAndUnzipReplay(replay);
            Dictionary<int, Hero> heroes = OpenDotaApi.GetMatchHeroes(match);
            int matchDurationSeconds = match.duration;

            PromptHeroSelection(heroes);        
            input = Console.ReadLine();
            int playerSlot;
            while (!int.TryParse(input, out playerSlot) && !heroes.Keys.Contains<int>(playerSlot))
            {
                PromptHeroSelection(heroes);
            }
            
            //handling dire heroes which start with slot #128
            if (playerSlot >= 128)
            {
                playerSlot -= 122;
            }
            System.Diagnostics.Debug.WriteLine("player slot is: " + playerSlot);

            LaunchDota();
            if (dota != null)
            {
                await StartReplay(matchId, playerSlot);
            }
            await StartObs();
            await WatchObs(matchDurationSeconds);
            StopObs();

            Console.ReadLine();
        }

        private static void InitializeWatcher()
        {
            watcher.Path = replayFolder;
            watcher.Created += new FileSystemEventHandler(OnAdd);
            watcher.EnableRaisingEvents = true;
        }

        private static void PromptHeroSelection(Dictionary<int, Hero> heroes)
        {
            Console.WriteLine("Please enter the hero you want to view as: ");
            foreach (var hero in heroes)
            {
                Console.WriteLine($"{hero.Key}: {hero.Value.localized_name}");
            }
        }

        private static void OnAdd(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("detected file change");
        }

        private static void LaunchDota()
        {
            Process.Start("C:\\Program Files (x86)\\Steam\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe");
            dota = Process.GetProcessesByName("dota2").FirstOrDefault();
        }

        private static async Task StartReplay(long matchId, int playerSlot)
        {
            IntPtr h = dota.MainWindowHandle;
            await Task.Delay(10000);
            Console.WriteLine("sending input..");
            System.Diagnostics.Debug.WriteLine("sending inputs...");

            var ahk = new AutoHotkey.Interop.AutoHotkeyEngine();
            ahk.LoadFile("../../test.ahk");
            ahk.ExecFunction("TestSend");
            ahk.ExecFunction("WatchThisReplay", matchId.ToString(), playerSlot.ToString());
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
            await Task.Delay(5000);
            SetForegroundWindow(h);
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

        public static void StopObs()
        {
            Debug.WriteLine("Stopping stream..at: " + DateTime.Now);
            obs = Process.GetProcessesByName("obs64").FirstOrDefault();

            IntPtr h = obs.MainWindowHandle;
            SetForegroundWindow(h);
            var ahk = new AutoHotkey.Interop.AutoHotkeyEngine();
            ahk.LoadFile("../../test.ahk");
            ahk.ExecFunction("StopStream");
        }
    }
}
