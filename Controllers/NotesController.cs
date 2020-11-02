using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web.Http;
using TravellingYuanWebAPI.Models;

namespace TravellingYuanWebAPI.Controllers
{
    public class NotesController : ApiController
    {
        private travellingyuanContext dbContext;

        public NotesController()
        {
            string connectionString = "server=travellingeurodb.mysql.database.azure.com;port=3306;user=travellingeuro@travellingeurodb;password=Gustavo98;database=tywebapi;sslmode=preferred";
            dbContext = DbContextFactory.Create(connectionString);
        }
        // GET: api/Notes/?serial=serialnumber returns an Uploads model( given its serial number incluing  the user email and the notes ID

        public IHttpActionResult Get([FromUri] string serial)
        {
            var result = dbContext.Notes.Where(s => s.SerialNumber == serial).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else
            {

                var q = result.Id;
                var qnotes = dbContext.Uploads.Include(u => u.Notes).Include(u => u.Users).
                    Select(e => new { e.Latitude, e.Longitude, e.Location, e.Address, e.Comments, e.Name, e.UploadDate, e.NotesId, e.Notes.SerialNumber, e.Notes.Value, e.Users.Email, e.Users.Keepmeinformed }).Where(n => n.NotesId == q);
                return Ok(qnotes);
            }
        }

        public IHttpActionResult GetEmailList([FromUri] string emaillist) //api/Notes/?emaillist = serialnumber
        {
            var result = dbContext.Notes.Where(s => s.SerialNumber == emaillist).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else
            {

                var q = result.Id;
                var r = dbContext.Uploads.Include(u => u.Notes).Include(u => u.Users).
                    Select(e => new { e.NotesId, e.Notes.SerialNumber, e.Users.Email, e.Users.Keepmeinformed }).Where(n => n.NotesId == q && n.Keepmeinformed == 1);
                return Ok(r);
            }
        }
        public IHttpActionResult GetLastEntry([FromUri] string last) //api/Notes/?last=serialnumber returns the last entry of a note given a serial number
        {
            var l = dbContext.Notes.Where(s => s.SerialNumber == last).FirstOrDefault();
            return Ok(l);
        }
        public IHttpActionResult Getchecknote([FromUri] string check) //api/notes/?check=serialnumber return the Id of a note given its serial number
        {
            var result = dbContext.Notes.Where(s => s.SerialNumber == check).Select(e => new { e.Id , e.Value});
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult GetIsSameValue([FromUri] string IsSameValue) //api/note/?issamevalue=serialnumber returns if the value of a given note is the same as a registered one
        {
            var result = dbContext.Notes.Where(s => s.SerialNumber == IsSameValue).Select(e => new { e.Value });
            return Ok(result);
        }

        public IHttpActionResult Getall()  //api/notes returns all the notes as entity
        {

            var result = dbContext.Uploads.Include(u => u.Notes).Include(u => u.Users).
                    Select(e => new {
                        e.Latitude,
                        e.Longitude,
                        e.Location,
                        e.Address,
                        e.Comments,
                        e.UploadDate,
                        e.NotesId,
                        e.Name,
                        e.Notes.SerialNumber,
                        e.Notes.Value,
                        e.Users.Email,
                        e.Users.Alias,
                        e.Users.Keepmeinformed
                    });
            if (result != null)
            {

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }





        public void Post([FromBody] Notes note) //api/notes  Upload a given note from Body of request
        {
            dbContext.Notes.Add(note);
            dbContext.SaveChanges();

        }

        // PUT: api/Notes/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Notes/5
        public void Delete(int id)
        {
        }
    }
}