using BryanPorter.SlackCmd.Models;

namespace BryanPorter.SlackCmd.CommandParsers
{
    public interface ICommandParser
    {
        bool TryParse(SlackRequest request, out Command command);
    }
}