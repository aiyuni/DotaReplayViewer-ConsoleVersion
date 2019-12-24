using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaReplayConsole
{
    class Replay
    {
        public long match_id { get; set; }
        public int cluster { get; set; }
        public int replay_salt { get; set; }
    }
}
