using entities.DataTransferObjects.JWTAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> GetUserByCode(string code);
        Task Update(int id, int compilanceStatusId, string objectType);
    }
}
