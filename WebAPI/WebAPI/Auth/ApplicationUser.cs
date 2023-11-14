﻿using System.ComponentModel;
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
        public int? company_id { get; set; }
        public string? employee_id { get; set; } 
        public ApplicationUser()
        {
            created_at = DateTime.Now; 
        }

    }
}
