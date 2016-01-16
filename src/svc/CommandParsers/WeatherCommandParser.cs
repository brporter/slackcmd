namespace BryanPorter.SlackCmd.CommandParsers
{
    public class WeatherCommandParser 
        : ICommandParser
    {
        public bool TryParse(string input, out Command command)
        {
            command = null;
            return true;
        }
    }
}