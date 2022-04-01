using entities.DataTransferObjects;
using entities.DataTransferObjects.JWTAuthentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        GetUserComplianceStatus GetUserByCode(string route);
        GetAllUserComplianceStatus GetAllUsers();
        Task<MemoryStream> GetAllUsersAsExcel();
        Task PutRequestComplience(int id, int compilanceStatusId, string objectType, string note);
        Task<MemoryStream> UsersToMemoryStream(IEnumerable<GetUserInfoDto> users);
        IEnumerable<GetUserInfoDto> GetCheckedUser(GetAllUserComplianceStatus statuses);
        Task<GetAllUserComplianceStatus> Search(string search, string searchType, int? complianceStatus, List<string> dates);

    }
}
