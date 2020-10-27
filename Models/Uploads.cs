using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravellingYuanWebAPI.Models
{
    public partial class Uploads
    {
        public int NotesId { get; set; }
        public int UsersId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Address { get; set; }
        public DateTime? UploadDate { get; set; }
        public string Location { get; set; }
        public string Comments { get; set; }
        public string Name { get; set; }

        public Notes Notes { get; set; }
        public Users Users { get; set; }
    }
}