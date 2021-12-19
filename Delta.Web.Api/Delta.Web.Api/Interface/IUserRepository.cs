namespace Delta.Web.Api
{ 
    public interface IUserRepository
    {
        Task<PagedList<UserDto>> GetUsersAysc(UserParams userParams);
        Task<AppUser?> GetUserByUsernameAsync(string? userName);
    }
}
