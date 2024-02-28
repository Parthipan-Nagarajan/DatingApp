namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

        bool ValidToken(string token);
    }
}