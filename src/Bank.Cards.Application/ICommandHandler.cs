using System.Threading.Tasks;
using Bank.Cards.Domain;

namespace Bank.Cards.Application
{
    public interface ICommandHandler<in T> where T : Command
    {
        Task<CommandExecutionResult> Handle(T command);
    }

    public abstract class CommandHandler<T> : ICommandHandler<T> where T : Command
    {
        public abstract Task<CommandExecutionResult> Handle(T command);

        protected CommandExecutionResult Ok() => CommandExecutionResult.Ok;
        protected CommandExecutionResult NotFound() => CommandExecutionResult.NotFound;

        protected CommandExecutionResult ValidationError(string errorMessage) => CommandExecutionResult.DomainValidationError(errorMessage);
    }
}