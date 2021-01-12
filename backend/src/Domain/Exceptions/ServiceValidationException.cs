namespace Domain.Exceptions
{
    public class ServiceValidationException : System.Exception, IException
    {
        public int HttpCode { get; private set; }

        public ServiceValidationException(string message)
            : base(message)
        {
            HttpCode = 400;
        }
    }
}
