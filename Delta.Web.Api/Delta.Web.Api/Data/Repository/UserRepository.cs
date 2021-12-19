using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Delta.Web.Api
{
    class UserRepository : IUserRepository
    {
        readonly IMapper _mapper;
        readonly DataContext _context;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PagedList<UserDto>> GetUsersAysc(UserParams userParams)
        {
            var query = _context.Users?.AsQueryable();
            query = query?.Where(u => u.UserName != userParams.CurrentUsername);
            query = userParams.OrderBy switch
            {
                "created" => query?.OrderByDescending(u => u.Created),
                _ => query?.OrderByDescending(u => u.LastActive)
            };
            return await PagedList<UserDto>.CreateAsync(
                query
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking(),
                userParams.PageNumber,
                userParams.PageSize);
        }
        public async Task<AppUser?> GetUserByUsernameAsync(string? userName)
        {
            var task = _context?.Users?.SingleOrDefaultAsync(x => x.UserName == userName);
            if (task != null) await task;
            return null;
        }
    }
}
