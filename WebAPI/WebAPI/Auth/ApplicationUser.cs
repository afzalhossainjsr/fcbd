using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(30)]
        public string? first_name { get; set; }
        [MaxLength(30)]
        public string? last_name { get; set; }
        public DateTime? date_of_birth { get; set; }
        public int? gender_id { get; set; }
        [DefaultValue(typeof(DateTime), "")]
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? user_image { get; set; }
        public ApplicationUser()
        {
            created_at = DateTime.Now; 
        }

    }
    public class RegisterModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsSocialLogin { get; set; } = false;
        public string? IdToken { get; set; }
        public string? Provider { get; set; }

    }
    public class Response
    {
        public string? status { get; set; }
        public string? message { get; set; }
    }
    public class FacebookUserInfo
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

}
