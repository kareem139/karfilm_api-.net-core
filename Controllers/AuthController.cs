using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OlaApi.Services;
using OlaApi.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OlaApi.Controllers
{
    [EnableCors("cor")]
    [Route("api/[controller]")]
    [ApiController]
   
    

    public class AuthController : ControllerBase
    {
        private readonly IUserService _UserService;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _singInManager;
        private IConfiguration _configuration;

        private RoleManager<IdentityRole> _roleManager;

        public AuthController(IUserService userService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _UserService = userService;
            _singInManager = singInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModels models)
        {
            if (ModelState.IsValid)
            {
                var result = await _UserService.RegisterUserAsync(models);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            else
            {
                return BadRequest("some prop not valid");
            }

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(loginviewmodel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new NullReferenceException("username  is empty");
            }

            var resul = await _userManager.CheckPasswordAsync(user, model.Password);
            if (resul)
            {

                    var role = await _userManager.GetRolesAsync(user);
                    

                    var claims = new Claim[]
                    {
                        new Claim("UserId",user.Id.ToString()),
                       
                        
                        new Claim("role",role.FirstOrDefault()),
                    };
                                        
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));


                    var sectoken = new JwtSecurityToken
                    (
                        issuer: _configuration["AuthSettings:Issuer"],
                        audience: _configuration["AuthSettings:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddDays(7),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(sectoken);
                    return Ok(new { token });
            }

            return BadRequest((new { message = "Username or password is incorrect." }));

        

        }

    }
}
