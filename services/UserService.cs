using contracts.Services;
using entities.DataTransferObjects;
using entities.DataTransferObjects.JWTAuthentication;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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
        public async Task UpdateStatusComplience(string route, int CompilanceStatusId, string note)
        {
            await Task.Run(() => {
                string preffix = code.Contains("s:") ? "service" : "products";
                return $"https://crm3.alif.tj/{preffix}/{code}";
            });
            return null;
        }

        public async Task Put(UpdateCompilanceStatus updateCompilanceStatus)
        {
            //updateCompilanceStatus.Response.Items.ComplianceStatusId = 1;
            var request = new RestRequest("http://192.168.15.170:7070/storage/documents/compliance/crm", Method.PUT);
            var client = new RestClient("http://192.168.15.170:7070/storage/documents/compliance/crm");

            request.AddHeader("Authorization", API_KEY);
            request.AddJsonBody(updateCompilanceStatus);
            var result = client.ExecuteGetAsync(request);
        }

        public async Task Update(int id, int compilanceStatusId, string objectType, string note)
        {
            string resource = $"http://192.168.15.170:7070/storage/documents/compliance/crm/{id}";
            var client = new RestClient(resource);
            var request = new RestRequest(resource, Method.GET);
            request.AddHeader("Authorization", API_KEY);
            var result = await client.ExecuteAsync<UpdateCompilanceStatus>(request);
            var updateCompilanceStatus = result.Data;

                    Int32.TryParse(address, out Id);
                }
            }

        

        //private async Task UpdateComplience(int id, int compilanceStatusId, string objectType)
        //{

        //    var client = new RestClient("http://192.168.15.170:7070/storage/documents/compliance/crm");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.PUT);
        //    request.AddHeader("Authorization", API_KEY);
        //    request.AddHeader("Content-Type", "application/json");
        //    var body = @"{
        //            " + "\n" +
        //                        @$"    ""id"":{id},
        //            " + "\n" +
        //                        @$"    ""object_type"": ""{objectType}"",
        //            " + "\n" +
        //                        @$"    ""compliance_status_id"":{compilanceStatusId}
        //            " + "\n" +
        //               @"}";
        //    request.AddParameter("application/json", body, ParameterType.RequestBody);
        //    IRestResponse response = await client.ExecuteAsync(request);
        //}

        private async Task UpdateStatusComplience(int id, int compilanceStatusId, string objectType, string note)
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
            Console.WriteLine(response.Content);
        }
    }
}
