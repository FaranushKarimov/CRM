using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.DataTransferObjects.ArchiveDTO
{
    public class GetAllWithFilterDTO
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        //public int compliance_status_id { get; set; }
        //public string type_search { get; set; }
        public string Search { get; set; }
        public List<DateTimeOffset> Date { get; set; }
    }
}
