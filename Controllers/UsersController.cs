using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravellingYuanWebAPI.Models;

namespace TravellingYuanWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private travellingyuanContext dbContext;

        public UsersController()
        {
            string connectionString = "server=127.0.0.1;port=3306;user=root;password=Gustavo98;database=tywebapi;sslmode=preferred";
            dbContext = DbContextFactory.Create(connectionString);
        }
        // GET: api/users?email=email returns a user model with email,confirmed and Id given an email

        public IHttpActionResult Getuserbyemail([FromUri] string email)
        {
            var result = dbContext.Users.Where(s => s.Email == email).Select(e => new {
                e.Email,
                e.EmailConfirmed,
                e.Id,
                e.Keepmeinformed,
                e.Keeplogged,
                e.Alias,
                e.Country,
                e.Role
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

        // GET: api/Users/alias/?=alias resturns a user model with the given alias
        public IHttpActionResult Getuserbyalias([FromUri] string alias)
        {
            var result = dbContext.Users.Where(s => s.Alias == alias).Select(e => new { e.Email, e.EmailConfirmed, e.Id, e.Keeplogged, e.Keepmeinformed, e.Country, e.Alias });
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        // POST: api/Users
        public void Postuser([FromBody] Users user) //api/User  Uplaod a given user from Body of request
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

        }

        // PUT: api/Users/?putuser=email
        public IHttpActionResult Putuser([FromUri] string putuser)
        {
            var target = dbContext.Users.SingleOrDefault(a => a.Email == putuser);
            if (target != null && ModelState.IsValid)
            {
                target.EmailConfirmed = 1;
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        //PUT api/users/

        public IHttpActionResult Putuserchanges(int id, [FromBody] Users userchanges)
        {

            var target = dbContext.Users.SingleOrDefault(a => a.Email == userchanges.Email);
            if (target != null && ModelState.IsValid)
            {
                target.Alias = userchanges.Alias;
                target.Country = userchanges.Country;
                target.Keepmeinformed = userchanges.Keepmeinformed;
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }


        }



    }
}

