
namespace BryanPorter.SlackCmd.CommandParsers
{
    using System;
    using BryanPorter.SlackCmd.Models;

    public class WeatherCommandParser 
        : IWeatherCommandParser
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
                    Arguments = request.Text.Split(' ')
                };

                return true;
            }

            return false;
        }
    }

    public interface IWeatherCommandParser
        : ICommandParser
    { }
}