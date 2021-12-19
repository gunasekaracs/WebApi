using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Delta.Web.Api
{
    [AllowAnonymous]
    public class AccountsController : BaseController
    {
        readonly DataContext _context;
        readonly ITokenService _tokenService;
        public AccountsController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if (_context == null || _context.Users == null) return BadRequest("Server error");
            var user = await _context!.Users!.SingleOrDefaultAsync(x => x.UserName == loginDto!.Username!.ToLower()); ;
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt!);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto!.Password!));
            for (int i = 0; i < computeHash.Length; i++)
                if (computeHash[i] != user.PasswordHash![i]) return Unauthorized("Invalid password");
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
        }
    }
}
