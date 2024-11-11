namespace Infrastructure.Exceptions
{
    public class CustomException : Exception
    {
        public readonly IDictionary<string, string[]> ExceptionMessages;

        public CustomException(IDictionary<string, string[]> exceptionMessages)
        {
            ExceptionMessages = exceptionMessages;
        }
    }
}
