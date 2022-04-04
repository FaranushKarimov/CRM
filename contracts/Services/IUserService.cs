﻿using entities.DataTransferObjects;
using entities.DataTransferObjects.ArchiveDTO;
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
        //Task<string> GetUserByCode(/*string code*/string route, int compilanceStatusId, string note);
        //Task UpdateStatusComplience(string route, int CompilanceStatusId, string note);
        //Task Update(int id, int compilanceStatusId, string objectType, string note);
        GetUserComplianceStatus GetUserByCode(string route);
        Task PutRequestComplience(int id, int compilanceStatusId, string objectType, string note);
        GetAllUserComplianceStatus GetUserListArchive(GetAllWithFilterDTO dto);


    }
}
