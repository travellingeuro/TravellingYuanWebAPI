using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web.Http;
using TravellingYuanWebAPI.Models;

namespace TravellingYuanWebAPI.Controllers
{
    public class UploadsController : ApiController
    {
        private travellingyuanContext dbContext;

        public UploadsController()
        {
            string connectionString = "server=travellingeurodb.mysql.database.azure.com;port=3306;user=travellingeuro@travellingeurodb;password=Gustavo98;database=tywebapi;sslmode=preferred";
            dbContext = DbContextFactory.Create(connectionString);
        }



        // GET: api/Uploads/?email=email returns all the uplaods of email.
        public IHttpActionResult GetbyEmail([FromUri] string email)
        {
            var result = dbContext.Users.Where(s => s.Email == email).FirstOrDefault(); ;
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                var q = result.Id;
                var uploads = dbContext.Uploads.Include(u => u.Notes).Include(u => u.Users).
                    Select(e => new { e.Latitude, e.Longitude, e.Location, e.Address, e.Comments, e.UploadDate, e.NotesId, e.Notes.SerialNumber, e.Notes.Value, e.Users.Email, e.UsersId }).Where(n => n.UsersId == q);
                return Ok(uploads);
            }
        }

        // POST: api/Uploads  upload a new upload given in the body of request

        public void Post([FromBody] Uploads upload)
        {
            dbContext.Uploads.Add(upload);
            dbContext.SaveChanges();

        }

        // PUT: api/Uploads/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Uploads/5
        public void Delete(int id)
        {
        }
    }
}