namespace BryanPorter.SlackCmd.CommandParsers
{
    public interface ICommandParser
    {
        bool TryParse(string input, out Command command);
    }
}