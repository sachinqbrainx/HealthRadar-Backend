using UserManagement.CommandModel;

namespace UserManagement.Commands.Interface
{
    public interface ITokenCommand
    {
        string CreateToken(RegistrationCommandModel user);
    }
}
