using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaReplayConsole.MatchDetailsProperties
{
    public class ConnectionLog
    {
        public int time { get; set; }
        public string @event { get; set; }
        public int player_slot { get; set; }
    }
}
