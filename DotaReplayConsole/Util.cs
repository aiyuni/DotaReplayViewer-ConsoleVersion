using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotaReplayConsole
{
    class Util
    {
        private static readonly NameValueCollection settings = ConfigurationManager.AppSettings;
        private static string replayFolder = settings["replayFolder"];

        public static void DownloadAndUnzipReplay(Replay replay)
        {
            string replayUrl = BuildReplayUrl(replay.match_id, replay.cluster, replay.replay_salt);
            string compressedFilePath = $"{replayFolder}\\{replay.match_id}.dem.bz2";
            string uncompressedFilePath = $"{replayFolder}\\{replay.match_id}.dem";
            string[] filePaths = Directory.GetFiles(replayFolder);

            foreach (string filePath in filePaths)
            {
                if (filePath.Equals(uncompressedFilePath))
                {
                    System.Diagnostics.Debug.WriteLine("Deleting file...");
                    File.Delete(filePath);
                }
            }

            using (WebClient webClient = new WebClient())
            {
                //File.Delete(Directory.GetFiles(replayFolder).Where(x => x.Equals(uncompressedFilePath)).FirstOrDefault());
                webClient.DownloadFile(new Uri(replayUrl), compressedFilePath);

                using (FileStream fs = new FileInfo(compressedFilePath).OpenRead())
                {
                    try
                    {
                        Debug.Write("starting to decompress");
                        BZip2.Decompress(fs, File.Create(uncompressedFilePath), true);
                    }
                    catch (Exception e)
                    {
                        Debug.Write("something went wrong");
                    }
                }
                Debug.Write("deleting compressed file...");
                File.Delete(Directory.GetFiles(settings["replayFolder"]).Where(x => x.Equals(compressedFilePath)).FirstOrDefault());
            }
        }

        private static string BuildReplayUrl(long matchId, int cluster, int replaySalt)
        {
            string url = cluster == 236 ? ".wmsj.cn / 570 /" : ".valve.net/570/";
            return $"http://replay{cluster}{url}{matchId}_{replaySalt}.dem.bz2";
        }
    }
}
