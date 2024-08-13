namespace ORM_Mini_Project.Exceptions;

public class WrongEmailException : Exception
{
    public WrongEmailException(string message) : base(message)
    {
        
    }
}
