using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BryanPorter.SlackCmd.CommandParsers
{
    public class Command
    {
        public string CommandText { get; set; }
        public string Preamble { get; set; }
        public IEnumerable<string> Arguments { get; set; } 
    }
}
