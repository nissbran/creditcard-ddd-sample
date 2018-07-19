using System.Threading.Tasks;

namespace Bank.Cards.Application
{
    public interface ICommandHandler<in T> where T : Command
    {
        Task<CommandExecutionResult> Handle(T command);
    }
}