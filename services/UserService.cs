using contracts.Services;
using entities.DataTransferObjects;
using entities.DataTransferObjects.JWTAuthentication;
using Newtonsoft.Json;
using RestSharp;
using SpreadCheetah;
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



        public async Task<MemoryStream> GetAllUsersAsExcel()
        {
            string resource = $"http://192.168.15.170:7070/storage/documents/compliance/crm/";
            var client = new RestClient(resource);
            var request = new RestRequest(resource, Method.GET);
            request.AddHeader("Authorization", API_KEY);
            request.AddHeader("Content-Type", "application/json"); 
            var queryResult = client.Execute(request).Content;
            var usersFromCrm = JsonConvert.DeserializeObject<GetAllUserComplianceStatus>(queryResult);
            var checkedUser = GetCheckedUser(usersFromCrm);
            return await UsersToMemoryStream(checkedUser);
        }

        public IEnumerable<GetUserInfoDto> GetCheckedUser(GetAllUserComplianceStatus statuses)
        {
            var result = new List<GetUserInfoDto>();
            result = statuses.Response.Items.Select(x => new GetUserInfoDto
            {
                Id = x.ID,
                City = x.Town,
                FullName = x.UserFullName,
                StatusName = x.ComplianceStatus.Name,
                ObjectType = x.ObjectType
            }).ToList();
            return result;
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


        //public GetAllUserComplianceStatus GetUserComplianceByName(string fullName)
        //{
        //    var allAdd = GetAllUsers();
        //    var result =  allAdd.Response.Items.SelectMany(x => x.UserFullName).ToList();
        //}

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

        public async Task<GetAllUserComplianceStatus> Search(string search, string searchType, int? complianceStatusId, List<string> dates)
        {
            var client = new RestClient("http://192.168.15.170:7070/");

            StringBuilder url = new StringBuilder("storage/documents/compliance/crm");
            string prefix = "/?";
            bool hasParam = false;
            if (search != null && searchType != null)
            {
                url.Append($"/?search={search}&type_search={searchType}");
                hasParam = true;
            }
            if(complianceStatusId != null)
            {
                url.Append($"{(hasParam ? "&" : prefix)}compliance_status_id={complianceStatusId}");
                hasParam = true;
            }
            if(dates != null)
            {
                foreach(var date in dates)
                {
                    url.Append($"{(hasParam ? "&" : prefix)}date={date}");
                    hasParam = true;
                }
            }
            var request = new RestRequest(url.ToString(), Method.GET);
            request.AddHeader("Authorization", "Bearer eyJqdGkiOiJqdGkiLCJleHAiOiIxOTQxMjM5MDIyIiwiaXNzIjoiaXNzIiwic3ViIjoiMDAzMzAyMjExIiwidXNlcm5hbWUiOiJDUk0iLCJpYXQiOjE2NDEyMzkwMjJ9");
            var queryResult = (await client.ExecuteAsync(request)).Content;
            return JsonConvert.DeserializeObject<GetAllUserComplianceStatus>(queryResult);
        }

        public async Task<MemoryStream> UsersToMemoryStream(IEnumerable<GetUserInfoDto> users)
        {
            var columns = new String[] { "Идентификационный номер", "ФИО", "Город", "Статус", "Объект" };
            await using var stream = new MemoryStream();
            using var spreadsheet = await Spreadsheet.CreateNewAsync(stream);
            await spreadsheet.StartWorksheetAsync("Транзакции");

            var row = new List<Cell>();
            foreach (var columnName in columns)
            {
                row.Add(new Cell(columnName));
            }

            await spreadsheet.AddRowAsync(row);

            row.Clear();
            foreach (var info in users)
            {
                row.Add(new Cell(info.Id));
                row.Add(new Cell(info.FullName));
                row.Add(new Cell(info.City));
                row.Add(new Cell(info.StatusName));
                row.Add(new Cell(info.ObjectType));
                await spreadsheet.AddRowAsync(row);
                row.Clear();
            }
            await spreadsheet.FinishAsync();
            return stream;
        }
    }
}
