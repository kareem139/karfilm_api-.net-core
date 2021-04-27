using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OlaApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace OlaApi.Controllers
{

   
    [EnableCors("cor")]
    [Route("api/[controller]")]
    [ApiController]
   
    
    

    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _UserService;
        private UserManager<IdentityUser> _userManager;
        public UserProfileController(IUserService UserService, UserManager<IdentityUser> userManager)
        {
            _UserService = UserService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetUserProfile")]
        [Authorize]
        public async Task<object> GetUserProfile()
        {
            var x = User.Claims.First(p=>p.Type== "UserId").Value;

            var user = await _userManager.FindByIdAsync(x);


            return new

            {
             user.Email,
             user.Id,
             user.UserName
             
            };

        }

        [HttpGet]
       
        [Authorize(Roles ="Admin")]
        [Route("admin")]
        
        public string admin()
        {
            return "admin page";
        }
        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("Customer")]
       
        public string customer()
        {
            return "customer page";
        }
    }
}
