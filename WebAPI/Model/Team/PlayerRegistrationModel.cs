using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Team
{
    public class PlayerRegistrationModel
    {
        public string? id { get; set; }
        public string? first_name_bn { get; set; }
        public string? last_name_bn { get; set; }
        public string? first_name_en { get; set; }
        public string? last_name_en { get; set; }
        public string? nick_name_bn { get; set; }
        public string? nick_name_en { get; set; }
        public string? date_of_birth { get; set; }
        public string? mobile_number { get; set; }
        public string? email { get; set; }
        public string? district_id { get; set; }
        public string? thana_id { get; set; }
        public string? player_position_id { get; set; }
        public string? jersey_number { get; set; }
        public string? profile_image { get; set; }
        public string? profile_image_base64 { get; set; } 
        public string? gender_id { get; set; }

    }
    public class PlayerRegistrationViewModel 
    {
        public string? id { get; set; }
        public string? first_name_bn { get; set; }
        public string? last_name_bn { get; set; }
        public string? first_name_en { get; set; }
        public string? last_name_en { get; set; }
        public string? nick_name_bn { get; set; }
        public string? nick_name_en { get; set; }
        public string? date_of_birth { get; set; }
        public string? height { get; set; }
        public string? weight { get; set; }
        public string? mobile_number { get; set; }
        public string? email { get; set; }
        public string? registration_number { get; set; }
        public string? district_id { get; set; }
        public string? thana_id { get; set; }
        public string? player_position_id { get; set; }
        public string? jersey_number { get; set; }
        public string? created_at { get; set; }
        public string? created_by { get; set; }
        public string? player_user_id { get; set; }
        public string? updated_at { get; set; }
        public string? updated_by { get; set; }
        public string? profile_image { get; set; }
        public string? gender_id { get; set; }
    }
}
