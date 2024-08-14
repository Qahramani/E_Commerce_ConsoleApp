using ORM_Mini_Project.Exceptions;

namespace ORM_Mini_Project.Utilities;

public static class Validations
{
    public static bool IsPasswordCorrect(string password)
    {
        if (password.Length < 8)
            throw new WrongPasswordException("Password Length should be >= 8");
        bool isUpper = false;
        bool isDigit = false;
        bool isLower = false;
        for (int i = 0; i < password.Length; i++)
        {
           char c = password[i];
            if (char.IsUpper(c))
                isUpper = true;
            else if (char.IsLower(c))
                isLower = true;
            else if (char.IsDigit(c))
                isDigit = true;
            else
                throw new WrongPasswordException("Password should consist of letter and digits");

        }
        if (isUpper && isLower && isDigit)
            return true;

        throw new WrongPasswordException("Password should contain\n - an upper letter\n - a lower letter\n - a digit");
    }

    public static void IsEmailCorrect(string email)
    {
        if (email.Length < 6)
            throw new WrongEmailException("email length should be at least 6 characters");
        int counter = 0;
        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == '@')
            {
                counter++;
            }
            else if (!char.IsLetter(email[i]) && !char.IsDigit(email[i]) && !(email[i] == '.'))
            {
                throw new WrongEmailException("email can contain only letters, numbers, underscore, dot and one @ tag");
            }
        }
        if (counter != 1)
            throw new WrongEmailException("email should contain 1 @ tag");
    }
}
