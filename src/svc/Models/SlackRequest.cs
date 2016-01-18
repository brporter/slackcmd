using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BryanPorter.SlackCmd.Models
{
    /// <summary>
    /// Represents the components of an incoming POST message from Slack.
    /// </summary>
    public class SlackRequest
    {
        public string Token { get; set; }

        public string TeamId { get; set; }

        public string TeamDomain { get; set; }

        public string ChannelId { get; set; }

        public string ChannelName { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Command { get; set; }

        public string Text { get; set; }

        public string ResponseUrl { get; set; }
    }
}
