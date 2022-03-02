using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.DataTransferObjects.JWTAuthentication
{
    public class AuthenticationResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
