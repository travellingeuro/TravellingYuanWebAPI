﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using TravellingYuanWebAPI.Models;

namespace TravellingYuanWebAPI.Controllers
{
    public class NotificationsController : ApiController
    {

        private travellingyuanContext dbContext;

        public NotificationsController()
        {
            string connectionString = "server=travellingeurodb.mysql.database.azure.com;port=3306;user=travellingeuro@travellingeurodb;password=*******;database=tywebapi;sslmode=preferred";
            dbContext = DbContextFactory.Create(connectionString);
        }


        public IHttpActionResult GetEmailList([FromUri] string serial) //api/Notifications/?serial=serial
        {
            var result = dbContext.Notes.Where(s => s.SerialNumber == serial).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                var q = result.Id;
                var r = dbContext.Uploads.Include(u => u.Notes).Include(u => u.Users).
                    Select(e => new { e.NotesId, e.Notes.SerialNumber, e.Users.Email, e.Users.Keepmeinformed }).Where(n => n.NotesId == q && n.Keepmeinformed == 1);

                var list = new List<string>();
                foreach (var item in r)
                {
                    list.Add(item.Email);
                }

                var Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/Notification.html"));
                Body = Body.Replace("#serial#", serial);
                SendEmail(Body, list);

                return Ok(r);

            }
        }
        public static void SendEmail(string Body, List<string> list)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@travellingyuan.com");
            foreach (var item in list)
            {
                message.To.Add(item);
            }
            message.Subject = "TravellingYuan new upload";
            message.IsBodyHtml = true;

            message.Body = Body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Host = "217.116.0.228";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("info@travellingyuan.com", "888888888");
            smtpClient.Send(message);


        }

    }
}