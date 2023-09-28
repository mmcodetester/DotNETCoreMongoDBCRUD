using DotNETCoreMongoDBCRUD.Controllers.Common;
using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.Entity.common;
using DotNETCoreMongoDBCRUD.Repository;
using DotNETCoreMongoDBCRUD.Utli;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNETCoreMongoDBCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        readonly AppSetting _appSetting;
        IReadWriteRepository<User> _userReadWriteRepo;
        public AuthController(IOptions<AppSetting> appSetting,IReadWriteRepository<User> userReadWriteRepo) 
        { 
            _appSetting = appSetting.Value;
            _userReadWriteRepo = userReadWriteRepo;
        }
        [HttpGet]
        [Route("login")]
        public JsonResult Login(string username,string password)
        {
            CommandResultModel result = new CommandResultModel();
            string tokenString = string.Empty;
            User user=_userReadWriteRepo.FindByName(username);

            if (user != null)
            {
                if(!PasswordHashHelper.ValidatePassword(password, user.password))
                {
                    user = null;
                    result.success = false;
                    result.messages.Add("Incorrect username or password");
                }
                else
                {
                    result.success=true;
                    result.messages.Add("Login Successfully!");

                }
            }
            if (result.success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                int hrs = DateTime.UtcNow.Hour - 1;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,user.name.ToString()),
                    //new Claim(ClaimTypes.Role,user.Role.name)
                    }),
                    Expires = DateTime.Now.AddHours(10), //DateTime.UtcNow.AddHours(24 - hrs), //DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                 tokenString = tokenHandler.WriteToken(token);
            }
            return Json(new {result=result,token=tokenString});
        }
    }
}
