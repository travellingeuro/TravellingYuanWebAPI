using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravellingYuanWebAPI.Models
{
    public partial class Notes
    {
        public Notes()
        {
            Uploads = new HashSet<Uploads>();
        }

        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Value { get; set; }
        public int MintsId { get; set; }

        public Mints Mints { get; set; }
        public ICollection<Uploads> Uploads { get; set; }
    }
}