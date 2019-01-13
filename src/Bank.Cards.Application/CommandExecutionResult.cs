namespace Bank.Cards.Application
{
    public sealed class CommandExecutionResult
    {
        public int Code { get; }
        public string Description { get; }

        private CommandExecutionResult(int code, string description)
        {
            Code = code;
            Description = description;
        }
        
        public static CommandExecutionResult Ok => new CommandExecutionResult(0, "Ok");
        public static CommandExecutionResult NotFound => new CommandExecutionResult(404, "NotFound");

        public static CommandExecutionResult DomainValidationError(string description)
        {
            return new CommandExecutionResult(400, description);
        }
    }
}