namespace ORM_Mini_Project.Exceptions;

public class UserIsBLokedException : Exception
{
    public UserIsBLokedException(string message) :  base(message)
    {
        
    }
}
