using DAL.Common;
using DAL.Repository.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Auth;

namespace WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly EmailConfiguration _emailConfig;
        private readonly IUserRegisterDAL _iUserRegisterDAL;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
          IConfiguration configuration, EmailConfiguration emailConfig, SignInManager<ApplicationUser> signInManager ,
           IUserRegisterDAL iUserRegisterDAL)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _emailConfig = emailConfig;
            _signInManager = signInManager;
            _iUserRegisterDAL = iUserRegisterDAL;
        }
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.PhoneNumber,
                first_name = model.first_name,
                last_name = model.last_name,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var phoneNumberToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                var messageResult = await _iUserRegisterDAL.SendOTPMessage(user.PhoneNumber,
                    "Your Football Bangla verification code is:\n" + phoneNumberToken);

                if (messageResult.statusCode == "200")
                {
                    return Ok(new Response
                    {
                        message = $"We have sent an verification code to your phone number {user.PhoneNumber}!",
                        status = "200"
                    });
                }

                return Ok(new Response { status = "Error", message = $"Error Status Code: {messageResult.statusCode}" });
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { status = "Error", message = "User creation failed! Please check user details and try again." });
        }

    }

}
