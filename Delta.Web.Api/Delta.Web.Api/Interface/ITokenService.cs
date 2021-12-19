namespace Delta.Web.Api
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
