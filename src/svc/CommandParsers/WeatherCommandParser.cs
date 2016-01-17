
namespace BryanPorter.SlackCmd.CommandParsers
{
    using System;
    using BryanPorter.SlackCmd.Models;

    public class WeatherCommandParser 
        : ICommandParser
    {
        const string CommandName = "/weather";

        public bool TryParse(SlackRequest request, out Command command)
        {
            command = null;

            if (request.Command.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
            {
                command = new Command()
                {
                    CommandText = request.Command,
                    Preamble = null,
                    Arguments = new[] { request.Text}
                };

                return true;
            }

            return false;
        }
    }
}