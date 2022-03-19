using contracts.Services;
using entities.DataTransferObjects;
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

        public GetAllUserComplianceStatus GetUserComplianceByName(string fullName)
        {
            throw new NotImplementedException();
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
        public async Task GetUserListArchive()
        {
            var url = "http://192.168.15.170:7070/storage/documents/compliance/crm?page=1&limit=25&compliance_status_id=6";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Headers["Authorization"] =   $"Bearer {API_KEY}";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            Console.WriteLine(httpResponse.StatusCode);



        }
    }
}
