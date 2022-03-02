using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.DataTransferObjects
{
    public class AlifBranch
    {
        public int id { get; set; }
        public int alif_id { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string town { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object parent_id { get; set; }
    }

    public class UserInfo
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string telegram { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public object about { get; set; }
        public object profession { get; set; }
        public string date_of_birth { get; set; }
        public string date_of_entry { get; set; }
        public int alif_branch_id { get; set; }
        public int user_profession_id { get; set; }
        public string phone_description { get; set; }
        public string second_phone { get; set; }
        public string second_phone_description { get; set; }
        public string third_phone { get; set; }
        public string third_phone_description { get; set; }
        public string address { get; set; }
        public int show_birthday { get; set; }
        public int created_from_id { get; set; }
        public string created_by_type { get; set; }
        public int created_by_id { get; set; }
        public int updated_from_id { get; set; }
        public string updated_by_type { get; set; }
        public int updated_by_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public AlifBranch alif_branch { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string api_token { get; set; }
        public int is_active { get; set; }
        public string remember_token { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string color { get; set; }
        public string text_color { get; set; }
        public string photo { get; set; }
        public int client_id { get; set; }
        public UserInfo user_info { get; set; }
    }

    public class Root
    {
        public User user { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
