using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OlaApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace OlaApi.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModels models);
        

    }


    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //public async Task<object> LoginUserAsync(loginviewmodel models)
        //{
        //   // var user = await _userManager.FindByNameAsync(models.Email);
        //   // if (user == null)
        //   // {
        //   //     throw new NullReferenceException("username  is empty");
        //   // }

        //   // var resul = await _userManager.CheckPasswordAsync(user, models.Password);
        //   // if (!resul)
        //   // {
        //   //     return new UserManagerResponse
        //   //     {
        //   //         Message = "invalid password",
        //   //         IsSuccess = false
        //   //     };
        //   // }

        //   // var claims = new[]
        //   // {
        //   //    new Claim("email",models.Email.ToString()),
              
        //   // };
        //   // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
        //   // var sectoken = new JwtSecurityToken
        //   //(
        //   //    issuer: _configuration["AuthSettings:Issuer"],
        //   //    audience: _configuration["AuthSettings:Audience"],
        //   //    claims: claims,
        //   //    expires: DateTime.Now.AddDays(7),
        //   //    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)

        //   //);
        //   // var token= new JwtSecurityTokenHandler().WriteToken(sectoken);
        //    return 0;
            
        //}

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModels models)
        {
            if (models==null)
            {
                throw new NullReferenceException("models is null");
            }

            if (models.Password !=models.ConfirmPassword)
            {
                
                return new UserManagerResponse
                {
                    Message = "password not match",
                    IsSuccess=false
                };
            }

            models.Role = "Customer";
            var identityuser = new IdentityUser
            {
                
                Email = models.Email,
                UserName = models.UserName
            };
            var result = await _userManager.CreateAsync(identityuser, models.Password);
            await _userManager.AddToRoleAsync(identityuser, models.Role);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "create success",
                    IsSuccess = true
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = "not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };

            }
        }
    }
}
