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
using System.Threading.Tasks;

namespace services
{
    public class UserService : IUserService
    {
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request_in)
        {
            try
            {
                
                string Resource = "https://apifront.alif.tj/api/auth/login";
                var client = new RestClient(Resource);
                var request = new RestRequest(Resource, Method.Post);


                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    email = request_in.Email,
                    password = request_in.Password
                }) ;

                RestResponse response = await client.ExecuteAsync(request);

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
            catch(Exception e)
            {
                return new AuthenticationResponse()
                {
                    Message = e.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<string> GetUserByCode(string code)
        {
            await Task.Run(() => {
                string preffix = code.Contains("s:") ? "service" : "product";
                return $"https://crm.alif.tj/{preffix}/{code}";
            });
            return null;
        }
    }
}
