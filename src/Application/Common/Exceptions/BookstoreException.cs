namespace BookStoreApi.Application.Common.Exceptions;

public class BookstoreException : Exception
{
    public BookstoreException(string message) : base(message)
    {
    }
}