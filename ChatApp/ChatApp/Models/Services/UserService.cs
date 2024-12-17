using ChatApp.Models.Common;
using ChatApp.Models.Response;
using ChatApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Models.Services
{


    /// <summary>
    /// This part is pending for changes
    /// </summary>
    /// 


    public class UserService
    {


        private readonly ChatAppContext _context;
        private readonly AppSettings _appSettings;

        public UserService(ChatAppContext context, IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
            _context = context;

        }

        public async Task<UserResponse> Auth(AuthViewmodel model, CancellationToken cancellationToken)
        {
            UserResponse userResponse = new UserResponse();



            var spassword = Encrypt.GetSHA256(model.Password);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == spassword, cancellationToken);

            if (user == null) return null;

            userResponse.Email = user.Email;
            userResponse.Token = GetToken(user);



            return userResponse;
        }


        public string GetToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Email)

                    }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

