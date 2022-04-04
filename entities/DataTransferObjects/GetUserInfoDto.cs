using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.DataTransferObjects
{
    public class GetUserInfoDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string StatusName { get; set; }
        public string ObjectType { get; set; }
    }
}
