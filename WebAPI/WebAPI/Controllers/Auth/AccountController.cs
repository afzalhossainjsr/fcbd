using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using DAL.Common;
using DAL.Repository.Auth;
using Facebook;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
          IConfiguration configuration, EmailConfiguration emailConfig, SignInManager<ApplicationUser> signInManager,
           IUserRegisterDAL iUserRegisterDAL, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _emailConfig = emailConfig;
            _signInManager = signInManager;
            _iUserRegisterDAL = iUserRegisterDAL;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            var u = await userManager.FindByNameAsync(model.PhoneNumber);

            if (u != null)
                return Ok(new Response { message = "User already exists!", status = "203" });

            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.PhoneNumber,
                first_name = model.first_name,
                last_name = model.last_name,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = false
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var phoneNumberToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                var messageResult = await SendOTP(user.PhoneNumber, "Your OTP code is:\n" + phoneNumberToken);

                if (messageResult.statusCode == "200")
                {
                    return Ok(new Response
                    {
                        message = $"We have sent  verification code to your phone number {user.PhoneNumber}!",
                        status = "200"
                    });
                }

                return Ok(new Response { status = "201", message = $"Message sending failed: {messageResult.statusCode}" });
            }
            return Ok(new Response { status = "202", message = "User creation failed! Please check user details and try again." });
        }

        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return Ok(new Response { message = "User not exists!", status = "201" });

            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userinfo = await userManager.FindByNameAsync(model.UserName);
                var userRoles = await userManager.GetRolesAsync(user);
                //var authClaims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, user.UserName),
                //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //};

                //authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetJWTToken(authClaims);

                return Ok(new
                {
                    userinfo = new {
                        UserFullName = user.first_name + " " + user.last_name,
                        FirstName = user.first_name,
                        LastName = user.last_name,
                        UserName = user.UserName,
                        MobileNumber = user.PhoneNumber,
                        UserImage = user.user_image,
                        Email = user.Email,
                        IsLoggedIn = true
                    },
                    JWTToken = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    status = "200",
                    message = "Login Successfully!"
                });
            }

            return Ok(new { message = "password does not match", status = "201" });
        }

        //[HttpPost]
        //[Route("login-user2")] 
        //public async Task<IActionResult> LoginUser2(LoginModel model) 
        //{
        //    var user = await userManager.FindByNameAsync(model.UserName);

        //    if (user == null)
        //    {
        //        return Ok(new Response { message = "User not exists!", status = "201" });
        //    }

        //    if (await userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //};

        //        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        //        // Create the identity for the user
        //        var claimsIdentity = new ClaimsIdentity(
        //            authClaims,
        //            CookieAuthenticationDefaults.AuthenticationScheme);

        //        // Create authentication properties for the persistent login session
        //        var authProperties = new AuthenticationProperties
        //        {
        //            AllowRefresh = true,
        //            IsPersistent = true, // Set to true for persistent login
        //            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) // Set cookie expiration time to 30 days
        //        };

        //        // Sign in the user with the created claims identity and auth properties
        //        await HttpContext.SignInAsync(
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIdentity),
        //            authProperties);

        //        return Ok(new
        //        {
        //            userinfo = new { UserName = user.UserName, Email = user.Email }, // Customize userinfo as needed
        //            status = "200",
        //            message = "Login Successfully!"
        //        });
        //    }

        //    return Ok(new { message = "Password does not match", status = "201" });
        //}

        [HttpPost]
        [Route("login-user2")]
        public async Task<IActionResult> LoginUser2(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Ok(new Response { message = "User not exists!", status = "404" });
            }


            if (!(user.PhoneNumberConfirmed))
            {
                var phoneNumberToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                var messageResult = await SendOTP(user.PhoneNumber,
                    "your  OTP code is:\n" + phoneNumberToken);
                if (messageResult.statusCode == "200")
                {
                    return Ok(new Response
                    {
                        message = $"We have sent  verification code to your phone number {user.PhoneNumber}!",
                        status = "300"
                    });
                }
                return Ok(new Response { status = messageResult.statusCode, message = $"Phone Number Not Confirmed! Message sending failed" });
            }

            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

                //// Create the identity for the user
                //var claimsIdentity = new ClaimsIdentity(
                //    authClaims,
                //    CookieAuthenticationDefaults.AuthenticationScheme);

                //// Create authentication properties for the persistent login session
                //var authProperties = new AuthenticationProperties
                //{
                //    AllowRefresh = true,
                //    IsPersistent = true, // Set to true for persistent login
                //    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) // Set cookie expiration time to 30 days
                //};

                //// Sign in the user with the created claims identity and auth properties
                //await HttpContext.SignInAsync(
                //    CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(claimsIdentity),
                //    authProperties);

                var JWTToken = GetJWTToken(authClaims);

                return Ok(new
                {
                        userinfo = new {
                        UserFullName = user.first_name + " " + user.last_name,
                            FirstName = user.first_name,
                            LastName = user.last_name,
                            UserName = user.UserName,
                        MobileNumber = user.PhoneNumber,
                        UserImage = user.user_image,
                        Email = user.Email,
                        IsLoggedIn = true,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                       
                        },
                    status = "200",
                    message = "Login Successfully!",
                    JWTToken = new JwtSecurityTokenHandler().WriteToken(JWTToken)
                });
            }

            return Ok(new Response { message = "Password does not match", status = "401" });
        }

        [HttpPost]
        [Route("GetSocialReferenceStatus")]
        public async Task<IActionResult> GetSocialReferenceStatus(AspNetUsersSocialUserReferenceSearchModel model)
        {
            string userId = await GetUserIdAsync(model);
            if (string.IsNullOrEmpty(userId))
            {
                return Ok(new
                {
                    message = "User not found!",
                    status = "201",
                    data = "no data"
                });
            }

            var result = await _iUserRegisterDAL.GetSocialReferenceStatus(model);

            if (result.Status == null)
            {
                return Ok(new
                {
                    message = "Some Error Occured!",
                    status = "201",
                    data = "no data"
                });
            }

            if ((model.Provider == "GOOGLE" && (result.Status == 1 || result.Status == 2)) ||
                (model.Provider == "FACEBOOK" && (result.Status == 1 || result.Status == 2)))
            {
                var user = await userManager.FindByNameAsync(result.PhoneNumber);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new
                    {
                        message = "Successfully login!",
                        status = "200",
                        data = await GetDynamicData(result)
                    });
                }
            }

            if ((model.Provider == "GOOGLE" && result.Status == 3) ||
                (model.Provider == "FACEBOOK" && result.Status == 3))
            {
                if (!string.IsNullOrEmpty(model.MobileNumber) && model.MobileNumber.Length > 10)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        Email = model.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = model.MobileNumber,
                        first_name = model.FirstName,
                        last_name = model.LastName,
                        PhoneNumber = model.MobileNumber,
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = !string.IsNullOrEmpty(model.Email) && model.Email.Length > 5
                    };

                    var result_user = await userManager.CreateAsync(user, "123456");
                    if (result_user.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok(new
                        {
                            message = "Successfully login!",
                            status = "200",
                            data = await GetDynamicData(result)
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            message = "User registration failed!",
                            status = "201",
                            data = "no data found"
                        });
                    }
                }
            }

            return Ok(new
            {
                message = "Some Error Occured!",
                status = "201",
                data = "no data found"
            });
        }

        private async Task<string> GetUserIdAsync(AspNetUsersSocialUserReferenceSearchModel model)
        {
            if (model.Provider == "GOOGLE")
            {
                return GetGoogleUserId(model.SocialUserId);
            }
            else if (model.Provider == "FACEBOOK")
            {
                var fbidresult = GetFacebookUser(model.SocialUserId);
                return fbidresult?.Id;
            }
            return null;
        }

        private async Task<dynamic> GetDynamicData(AspNetUsersSocialUserReferenceViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GetJWTToken(authClaims);


            var data = new
            {
                userinfo = user,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                status = "200",
                message = "Login Successfully!"
            };
            return data;
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {

                    return Ok(new
                    {
                        status = "200",
                        message = "Confirm Successfully!"
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "201", message = "This User OTP Failed!" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "201", message = "This User Doesnot exist!" });
        }

        [HttpPost("ConfirmPhoneNumber")]
        public async Task<IActionResult> ConfirmPhonenumber(ConfirmPhoneNumberModel obj)
        {
            var user = await userManager.FindByNameAsync(obj.PhoneNumber);
            if (user != null)
            {
                var result = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, obj.Token);
                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        status = "200",
                        message = "Confirm Successfully!"
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "201", message = "This User OTP Failed!" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { status = "202", message = "This User Doesnot exist!" });
        }

        private JwtSecurityToken GetJWTToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(365),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
           
            return token;
        }

        private string GetGoogleUserId(string? idToken)
        {
            try
            {
                var payload = GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings()).Result;
                return payload.Subject;
            }
            catch (Exception ex)
            {

                return "Exception Occured Ex:" + ex.Message.ToString();
            }
        }

        private FacebookUserInfo GetFacebookUser(string? _accessToken)
        {
            FacebookUserInfo userinfo = new FacebookUserInfo();
            var client = new FacebookClient(_accessToken);
            try
            {
                var result = client.Get("/me?fields=id,email");
                var responseObj = JObject.Parse(result.ToString());
                userinfo = new FacebookUserInfo()
                {
                    Id = responseObj["id"]?.ToString(),
                    Email = responseObj["email"]?.ToString()
                };
            }
            catch (Exception ex)
            {
                userinfo.Email = ex.Message.ToString();
                return userinfo;
            }

            return userinfo;
        }
        private string ReturnSMSToken()
        {
            string Letter = @"1234567890";
            var code = "";
            for (int i = 0; i < 6; i++)
            {
                var rn = new Random();
                int randomInt = rn.Next(1, 10);
                code += Letter.Substring(randomInt - 1, 1);
            }
            return code;
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            OTPMessageResultModel messageResult = new OTPMessageResultModel() { message = "", statusCode = "" };
            var user = await userManager.FindByNameAsync(model.MobileNumber);
            if (user == null)
            {
                return Ok(new Response
                {
                    message = "User not found.",
                    status = "202"
                });
            }
            var token = ReturnSMSToken();
            var passwordtoken = await userManager.GeneratePasswordResetTokenAsync(user);
            //Save Message To Database 
            var forgotPasswordObj = new ForgotPasswordToken()
            {
                PhoneNumber = model.MobileNumber,
                PasswordToken = passwordtoken,
                SMSToken = token
            };
            var saveResult = await _iUserRegisterDAL.SaveForgotPasswordToken(forgotPasswordObj);

            if (int.Parse(saveResult?.ResultID) > 0)
            {
                messageResult = await SendOTP(user.PhoneNumber, "Your OTP code is:\n" + token);
            }
            else
            {
                return Ok(new Response
                {
                    message = $"An Error Occured!",
                    status = "203"

                });
            }
            if (messageResult.statusCode == "200")
            {
                return Ok(new Response
                {
                    message = $"We have sent an OTP to your phonenumber {user.PhoneNumber}!",
                    status = "200"
                });
            }
            return Ok(new Response { status = messageResult.statusCode, message = "Message Sending Failed!" });
        }

        [HttpPost]
        [Route("GetVerifyToken")]
        public async Task<IActionResult> GetVerifyToken(ResetPasswordModel resetPasswordModel)
        {
            var result = await _iUserRegisterDAL.GetVerifyToken(resetPasswordModel);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var result = await _iUserRegisterDAL.GetForgotPasswordToken(resetPasswordModel);
            var user = await userManager.FindByNameAsync(result.PhoneNumber);
            if (user == null)
                return Ok(new OTPMessageResultModel
                {
                    message = "User not found.",
                    statusCode = "201"
                });
            var resetPassResult = await userManager.ResetPasswordAsync(user, result.PasswordToken, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                return Ok(new
                {
                    message = "Failed to reset password.",
                    status = "201"
                });
            }
            return Ok(new
            {
                message = "Successfully reset your password.",
                status = "200"
            });
        }
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswortAsync(ChangePasswordModel model)
        {
            string claim = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            var user = await userManager.FindByNameAsync(claim);
            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return Ok(new Response
                {
                    message = "Change Passwor failed!",
                    status = "201"
                });
            }
            return Ok(new Response
            {
                message = "Change Passwor successfully!",
                status = "200"
            });
        }
        [HttpGet]
        [Route("GetSMSToken")]
        public async Task<IActionResult> GetSMSToken(string MobileNumber)
        {
            AspNetSocialUserVerificationToken lst = new AspNetSocialUserVerificationToken();
            SMSTokenModel obj = new SMSTokenModel() { MobileNumber = MobileNumber, SMSToken = ReturnSMSToken() };
            var messageResult = await _iUserRegisterDAL.SendOTPMessage(MobileNumber,
                "Your football Bangla OTP Code is:\n" + obj.SMSToken.ToString());
            if (messageResult.statusCode == "200")
            {
                lst = await _iUserRegisterDAL.GetSMSToken(obj);
                lst.SMSToken = (lst.SMSToken);
                lst.SMSStatus = "200";
            }
            else
            {
                lst.MobileNumber = MobileNumber;
                lst.SMSStatus = messageResult.statusCode;
            }

            return new JsonResult(lst);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult("Success");
        }


        [HttpGet("CheckLoginStatus")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CheckLoginStatus()
        {
            var isAuthenticated = _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated;
            return Ok(new { status = isAuthenticated.HasValue && isAuthenticated.Value ? 1 : 0 });
        }
        [HttpGet("IsLoggedIn")]
        public bool IsLoggedIn()
        {
            var isAuthenticated = HttpContext?.User?.Identity?.IsAuthenticated;
            return isAuthenticated.HasValue && isAuthenticated.Value;
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo() 
        {
            var isAuthenticated = HttpContext?.User?.Identity?.IsAuthenticated;
            var UserName2 = User?.Identity?.Name;
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
               
                var userName = HttpContext?.User?.Identity?.Name;
                var u = await userManager.FindByNameAsync(userName);
                return Ok(new
                {
                    userinfo  = new { 
                        UserFullName = u.first_name+ " " + u.last_name,
                        FirstName = u.first_name,
                        LastName = u.last_name,
                        UserName = u.UserName,
                        MobileNumber = u.PhoneNumber,
                        UserImage = u.user_image,
                        Email= u.Email,
                        IsLoggedIn = isAuthenticated
                    },
                    status = "200",
                    message = "Loggedin User!"
                });
            }
            return Ok(new
            {
                userinfo = new
                {
                    UserFullName = "",
                    FirstName = "",
                    LastName = "",
                    UserName = "",
                    MobileNumber ="",
                    UserImage = "",
                    Email = "", 
                    IsLoggedIn = isAuthenticated 
                },
                status = "201",
                message = "No User Found!"
            });
        }

        private async Task<OTPMessageResultModel> SendOTP(string phonenumber, string messageText)
        {
            OTPMessageResultModel objResult = new OTPMessageResultModel() { statusCode = "", message = "-1" };

            var data = new   { phoneNumber = phonenumber,  message  = messageText };

            try
            {
                var jsonPayload = JsonConvert.SerializeObject(data);
                using (var client = new HttpClient())
                {
                    var apiUrl = @"https://userapi.looks.com.bd/api/UserProfile/SendSingleMessage"; // Replace with your API endpoint URL
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<OTPMessageResultModel>(responseContent);
                    if (result != null)
                    {
                        objResult = result;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return await Task.Run(() => objResult);
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var UserName = HttpContext.User?.Identity?.Name;
            if (UserName!=null)
            {
                var u = await userManager.FindByNameAsync(UserName);
                var profile = new
                {
                    FirstName = u.first_name,
                    LastName = u.last_name,
                    Email = u.Email,
                    DOB = u.date_of_birth,
                    GenderId = u.gender_id,
                    CompanyId = u.company_id,
                    EmpId = u.employee_id ,
                    UserImage = u.user_image,
                    MobileNumber = u.PhoneNumber,

                };
                return Ok(new
                {
                    userinfo = profile,
                    status = "200",
                    message = "No User Found!"
                });
            }
            return new JsonResult(new { userinfo = new { }, status ="201", message = "user not found" });
        }


        [HttpPost]
        [Route("SaveUserProfile")]
        public async Task<IActionResult> SaveUserProfile(UserPofileModel obj)  
        {
            var UserName = HttpContext.User?.Identity?.Name;
            if (UserName != null)
            {
                var user = await userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return Ok(new Response
                    {
                        status = "204",
                        message = "No User Found!"
                    });
                }

                user.Email = obj.email;
                user.date_of_birth = obj.date_of_birth;
                user.first_name = obj.first_name;
                user.last_name = obj.last_name;
                user.gender_id = obj.gender_id;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new Response
                    {
                        status = "200",
                        message = "Successfully updated profile!"
                    });
                }
            }
            return new JsonResult(new { userinfo = new { }, status = "201", message = "Update profile failed!" });
        }

        [HttpPost]
        [Route("SaveUserAddress")]
        public async Task<IActionResult> SaveUserAddress(UserAddressModel obj) 
        {
            var UserName = HttpContext.User?.Identity?.Name;
            var result = await _iUserRegisterDAL.SaveUserAddress(UserName,obj);
            return new JsonResult(result);

        }
        [HttpPost]
        [Route("UpdateUserAddress")]
        public async Task<IActionResult> UpdateUserAddress(UserAddressModel obj) 
        {
            var UserName = HttpContext.User?.Identity?.Name;
            var result = await _iUserRegisterDAL.UpdateUserAddress(UserName, obj);
            return new JsonResult(result);

        }
        [HttpGet]
        [Route("GetUserAddress")]
        public async Task<IActionResult> GetUserAddress() 
        {
            var UserName = HttpContext.User?.Identity?.Name;
            var result = await _iUserRegisterDAL.GetUserAddress(UserName);
            return new JsonResult(result);

        }

    }
}
