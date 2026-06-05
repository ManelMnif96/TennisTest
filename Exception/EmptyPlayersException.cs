namespace AtelierTest.Exceptions;

public class EmptyPlayersException : Exception
{
    public EmptyPlayersException()
        : base("No players available in the system.")
    {
    }
}