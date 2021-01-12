namespace Domain.Exceptions
{
    public class EntityNotFoundException : System.Exception, IException
    {
        public int HttpCode { get; private set; }

        public EntityNotFoundException(string message)
            : base(message)
        {
            HttpCode = 404;
        }
    }
}
