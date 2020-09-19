using System.Net.Http;
using System.Web.Http;
using TravellingYuanWebAPI.Models;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace TravellingYuanWebAPI.Controllers
{
    public class SMSController : ApiController
    {
        private HttpClient _client = new HttpClient();
        private string result;



        // POST: api/SMS

        public IHttpActionResult PostSMS([FromBody] SMS sms)

        {
            SendSms(sms);
            return Ok(result);
        }

        private void SendSms(SMS sms)

        {

            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "ACd3abf35948c5eef2a05ed1801e730f3a";
            const string authToken = "2a3fcbef5c5df0274741357817a67f4c";

            var To = sms.To;
            var Body = sms.Body;

            TwilioClient.Init(accountSid, authToken);

            try
            {

                MessageResource message = MessageResource.Create(
                    body: Body,
                    from: new Twilio.Types.PhoneNumber("+37128914196"),
                    to: new Twilio.Types.PhoneNumber(To)
                );

                result = message.Status.ToString();
            }

            catch (ApiException e)
            {

                result = $"Twilio Error {e.Code} - {e.MoreInfo}";
            }

        }

    }
}

