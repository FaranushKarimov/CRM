using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.DataTransferObjects
{
    
        public class MetaDto
        {
            [JsonProperty("error")]
            public bool Error { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("status_code")]
            public int StatusCode { get; set; }
        }

        public class Pagination
        {
            [JsonProperty("total")]
            public int Total { get; set; }

            [JsonProperty("current_page")]
            public int CurrentPage { get; set; }

            [JsonProperty("last_page")]
            public int LastPage { get; set; }

            [JsonProperty("from")]
            public int From { get; set; }

            [JsonProperty("to")]
            public int To { get; set; }
        }

        public class ComplianceStatusDto
        {
            [JsonProperty("ID")]
            public int ID { get; set; }

            [JsonProperty("CreatedAt")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("UpdatedAt")]
            public DateTime UpdatedAt { get; set; }

            [JsonProperty("DeletedAt")]
            public object DeletedAt { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }
        }

        public class Item
        {
            [JsonProperty("ID")]
            public int ID { get; set; }

            [JsonProperty("CreatedAt")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("UpdatedAt")]
            public DateTime UpdatedAt { get; set; }

            [JsonProperty("DeletedAt")]
            public object DeletedAt { get; set; }

            [JsonProperty("object_type")]
            public string ObjectType { get; set; }

            [JsonProperty("user_full_name")]
            public string UserFullName { get; set; }

            [JsonProperty("user_id")]
            public int UserId { get; set; }

            [JsonProperty("town")]
            public string Town { get; set; }

            [JsonProperty("town_id")]
            public int TownId { get; set; }

            [JsonProperty("compliance_status_id")]
            public int ComplianceStatusId { get; set; }

            [JsonProperty("compliance_status")]
            public ComplianceStatus ComplianceStatus { get; set; }

            [JsonProperty("note")]
            public string Note { get; set; }
        }

        public class ResponseDto
        {
            [JsonProperty("pagination")]
            public Pagination Pagination { get; set; }

            [JsonProperty("items")]
            public List<Item> Items { get; set; }
        }

        public class GetAllUserComplianceStatus
        {
            [JsonProperty("meta")]
            public MetaDto Meta { get; set; }

            [JsonProperty("response")]
            public ResponseDto Response { get; set; }
        }

    
}
