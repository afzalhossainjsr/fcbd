using System.Net;
using DAL.Repository.Auth;
using DAL.Repository.Team;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Auth;
using Model.Team;

namespace WebAPI.Controllers.Team
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IPlayerRegistrationDal _iteamRegisterDAL;
        public TeamController(
           IPlayerRegistrationDal iUserRegisterDAL)
        {

            _iteamRegisterDAL = iUserRegisterDAL; 
        }
        [HttpPost]
        [Route("register-player")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RegisterPlayer(PlayerRegistrationModel obj)
        {
              var user_name=   HttpContext?.User?.Identity?.Name;
              var result =   await _iteamRegisterDAL.SavePlayer(user_name,obj);
            return new JsonResult(result);  
        }
     }
}
