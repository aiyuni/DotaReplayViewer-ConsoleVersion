using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaReplayConsole.MatchDetailsProperties
{
    public class Chat
    {
        public int time { get; set; }
        public string unit { get; set; }
        public string key { get; set; }
        public int slot { get; set; }
        public int player_slot { get; set; }
    }
}
