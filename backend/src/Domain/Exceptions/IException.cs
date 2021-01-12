namespace Domain.Exceptions
{
    public interface IException
    {
        string Message { get; }
        int HttpCode { get; }
    }
}
