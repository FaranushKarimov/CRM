using contracts.Services;
using entities.DataTransferObjects;
using entities.DataTransferObjects.ArchiveDTO;
using entities.DataTransferObjects.JWTAuthentication;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace services
{
    public class UserService : IUserService
    {
        private const string API_KEY = "Bearer eyJqdGkiOiJqdGkiLCJleHAiOiIxOTQxMjM5MDIyIiwiaXNzIjoiaXNzIiwic3ViIjoiMDAzMzAyMjExIiwidXNlcm5hbWUiOiJDUk0iLCJpYXQiOjE2NDEyMzkwMjJ9";

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request_in)
        {
            try
            {
                string Resource = "https://apifront.alif.tj/api/auth/login";
                var client = new RestClient(Resource);
                var request = new RestRequest(Resource, Method.POST);


                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    email = request_in.Email,
                    password = request_in.Password
                });

                RestResponse response = (RestResponse)await client.ExecuteAsync(request);

                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);

                return new AuthenticationResponse()
                {
                    //Message = JsonConvert.SerializeObject(System.Text.RegularExpressions.Regex.Unescape(response.Content)),
                    Message = "Success",
                    Token = myDeserializedClass.access_token,
                    StatusCode = (int)response.StatusCode,
                    Exriration = myDeserializedClass.expires_in
                };
            }
            catch (Exception e)
            {
                return new AuthenticationResponse()
                {
                    Message = e.Message,
                    StatusCode = 500
                };
            }
        }

        public GetAllUserComplianceStatus GetAllUsers()
        {
            string resource = $"http://192.168.15.170:7070/storage/documents/compliance/crm/";
            var client = new RestClient(resource);
            var request = new RestRequest(resource, Method.GET);
            request.AddHeader("Authorization", API_KEY);
            request.AddHeader("Content-Type", "application/json");
            var queryResult = client.Execute(request).Content;
            return JsonConvert.DeserializeObject<GetAllUserComplianceStatus>(queryResult);
        }

        public GetUserComplianceStatus GetUserByCode(string route)
        {
            string[] linkList = route.Split("/");
            int id = int.Parse(linkList[4]);
            string resource = $"http://192.168.15.170:7070/storage/documents/compliance/crm/{id}";
            var client = new RestClient(resource);
            var request = new RestRequest(resource, Method.GET);
            request.AddHeader("Authorization", API_KEY);
            request.AddHeader("Content-Type", "application/json");
            var queryResult = client.Execute(request).Content;
            return JsonConvert.DeserializeObject<GetUserComplianceStatus>(queryResult);
        }

        //public async Task UpdateStatusComplience(string route, int CompilanceStatusId, string note)
        //{
        //    string ObjectType = default;
        //    int Id = default;
        //    route = route.Replace("https://crm3.alif.tj/", "");

        //    if (route.ToLower().Contains("product"))
        //    {
        //        ObjectType = "product";
        //        var address = Regex.Match(route, @"\d+").Value;

        //        Int32.TryParse(address, out Id);
        //    }
        //    else
        //    {
        //        if (route.ToLower().Contains("services"))
        //        {
        //            ObjectType = "service";
        //            var address = Regex.Match(route, @"\d+").Value;

        //            Int32.TryParse(address, out Id);
        //        }
        //    }

        //    await PutRequestComplience(Id, CompilanceStatusId, ObjectType, note);
        //}
        public async Task PutRequestComplience(int id, int compilanceStatusId, string objectType, string note)
        {
            var client = new RestClient($"http://192.168.15.170:7070/storage/documents/compliance/crm/{objectType}/{id}");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", API_KEY);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
            " + "\n" +
            @$"    ""compliance_status_id"":{compilanceStatusId},
            " + "\n" +
            @$"    ""note"":""{note}""
            " + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
        }
        //{
        //  "page": 1,
        //  "limit": 25,
        //  "search": "Ғуломидидинов",
        //  "date": [
        //    "2022-03-06T07:22:36.089Z",
        //    "2022-03-31T07:22:36.089Z"
        //  ]
        //}
        public GetAllUserComplianceStatus GetUserListArchive(GetAllWithFilterDTO dto)
        {
            string ServerUrl = $"http://192.168.15.170:7070/storage/documents/compliance/crm/?";
            ServerUrl = CreateLink(dto.Search, ServerUrl);
            ServerUrl = GetDate(ServerUrl, dto.Date);
            ServerUrl = ServerUrl + "&compliance_status_id=6";
            string Content = SendGetQuery(ServerUrl);
            return JsonConvert.DeserializeObject<GetAllUserComplianceStatus>(Content);
        }
        private string GetDate(string ServerUrl, List<DateTimeOffset> DateList)
        {
            if (DateList == null)
                return ServerUrl;
            foreach (var date in DateList)
            {
                ServerUrl = ServerUrl + "&" + $"date={date.Date.ToString("yyyy.MM.dd")}";
            }
            return ServerUrl;
        }
        private string CreateLink(string searchString, string ServerUrl)
        {
            int SearchInt;
            if (searchString != null)
            {
                var search = Regex.Match(searchString, @"\d+").Value;
                Int32.TryParse(search, out SearchInt);
                string type_search;
                if (SearchInt.GetType().FullName.Contains("Int") && SearchInt != 0)
                {
                    type_search = "object_id";
                }
                else
                    type_search = "username";
                ServerUrl = ServerUrl + "&" + $"type_search={type_search}" + $"&search={searchString}";
            }
            return ServerUrl;
        }
        private string SendGetQuery(string ServerUrl)
        {
            var client = new RestClient(ServerUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", API_KEY);
            var response = client.Execute(request).Content;
            return response;
        }
    }
}
