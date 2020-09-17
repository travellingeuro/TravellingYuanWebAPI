using System.Linq;
using System.Web.Http;
using TravellingYuanWebAPI.Models;

namespace TravellingYuanWebAPI.Controllers
{
    public class MintsController : ApiController
    {
        private travellingyuanContext dbContext;

        public MintsController()
        {

        }

        // GET: api/Mints/?code=code returns a Mint model given its code in the URI
        public IHttpActionResult GetMintByCode([FromUri] string code)
        {
            string connectionString = "server=127.0.0.1;port=3306;user=root;password=Gustavo98;database=tywebapi;sslmode=preferred";
            dbContext = DbContextFactory.Create(connectionString);
            var result = dbContext.Mints.Where(s => s.Mintcode == code).
            Select(e => new { e.Latitude, e.Longitude, e.Location, e.Address, e.Comments, e.Id });
            return Ok(result);
        }


        //GET de Prueba 

        public IHttpActionResult Get()
        {
            var result= new string[] { "puta", "madre" };
            return Ok(result);
        }

        // POST: api/Mints
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Mints/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Mints/5
        public void Delete(int id)
        {
        }
    }
}
