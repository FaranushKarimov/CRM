using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.DataTransferObjects
{
       public class Meta
        {
            [JsonProperty("error")]
            public bool Error { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("status_code")]
            public int StatusCode { get; set; }
        }

        public class ComplianceStatus
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

        public class Items
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
        }

        public class Response
        {
            [JsonProperty("items")]
            public Items Items { get; set; }
        }

        public class UpdateCompilanceStatus
        {
            [JsonProperty("meta")]
            public Meta Meta { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }

    }
