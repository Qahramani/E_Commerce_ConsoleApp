namespace ORM_Mini_Project.Exceptions;

public class WrongPasswordException  :Exception
{
    public WrongPasswordException(string message) : base(message)
    {
        
    }
}
