using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackOfScouts
{
    internal class ApplicationState
    {
        internal List<MatchData>? Data { get; set; }
        internal List<ScheduleEntry>? Entries { get; set; }
    }
}
