namespace AtelierTest.Exceptions;

public class InvalidPlayerDataException : Exception
{
    public InvalidPlayerDataException(string message)
        : base(message)
    {
    }
}