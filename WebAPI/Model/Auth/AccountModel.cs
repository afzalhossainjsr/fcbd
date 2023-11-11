using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Auth
{
    public class OTPMessageModel
    {

        public string? username { get; set; }
        public string? password { get; set; }
        public string? apicode { get; set; }
        public string? msisdn { get; set; }
        public string? countrycode { get; set; }
        public string? cli { get; set; }
        public string? messagetype { get; set; }
        public string? message { get; set; }
        public string? messageid { get; set; }
    }
    public class OTPBulkMessageModel
    {

        public string? username { get; set; }
        public string? password { get; set; }
        public string? apicode { get; set; }
        public string[]? msisdn { get; set; }
        public string? countrycode { get; set; }
        public string? cli { get; set; }
        public string? messagetype { get; set; }
        public string? message { get; set; }
        public string? messageid { get; set; }
    }
    public class OTPMessageResultModel
    {

        public string? statusCode { get; set; }
        public string? message { get; set; }

    }
    public class EmailConfiguration
    {
        public string? From { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? message_username { get; set; }
        public string? message_password { get; set; }
        public string? message_cli { get; set; }
        public string? message_countrycode { get; set; }
    }
    public class UserRegistrationInfoModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumberPrefix { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? UserImage { get; set; }

    }
    public class AspNetUsersSocialUserReferenceViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Status { get; set; }

    }
    public class AspNetUsersSocialUserReferenceSearchModel
    {
        public string? SocialUserId { get; set; }
        public string? Provider { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class ForgotPasswordToken
    {
        public string PhoneNumber { get; set; }
        public string PasswordToken { get; set; }
        public string SMSToken { get; set; }

    }
    public class VerifyTokenModel
    {
        public string? SMSToken { get; set; }

    }
    public class ResetPasswordModel
    {
        public string Password { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class ConfirmPhoneNumberModel
    {
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class ChangePasswordModel
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
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
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ForgotPasswordModel
    {

        public string MobileNumber { get; set; }
    }
    public class AspNetSocialUserVerificationToken 
    {
        public int? Id { get; set; }
        public string? MobileNumber { get; set; }
        public int? SMSToken { get; set; }
        public string? CreatedAt { get; set; }
        public string? SMSStatus { get; set; }
    }
    public class SMSTokenModel
    {
        public string? MobileNumber { get; set; }
        public string? SMSToken { get; set; }
    }
}
