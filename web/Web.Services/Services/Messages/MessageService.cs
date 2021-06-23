using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppApi;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Web.Services.Services.Messages
{
    public interface IMessageService
    {
        void SendMessageOnWhatsApp(string toNumber, string message);
    }

    public class MessageService:IMessageService
    {

        public void SendMessageOnWhatsApp(string toNumber,string message)
        {
            //if (!toNumber.Contains('+'))
            //    toNumber = "+977" + toNumber;
            //System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=" + toNumber + "&text=" + message);

            //TwilioClient.Init(
            //    Environment.GetEnvironmentVariable("sanzoosunar123@gmail.com"),
            //    Environment.GetEnvironmentVariable("SanjuktaSunar4567")
            //    );

            //MessageResource.Create(
            //    from:new PhoneNumber("whatsapp:+9779821577495"),
            //    to:new PhoneNumber("whatsapp:+977 9857078804"),
            //    body:"Hello world"
            //    );

            //string accountSid = Environment.GetEnvironmentVariable("ACc78a77aae385db99bd8cfed29cb16f04");
            //string authToken = Environment.GetEnvironmentVariable("5f80f120a053726129267de1c58d8127");

            string accountSid = "ACc78a77aae385db99bd8cfed29cb16f04";
            string authToken = "5f80f120a053726129267de1c58d8127";
            TwilioClient.Init(accountSid, authToken);

            MessageResource.Create(
                body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                //from: new Twilio.Types.PhoneNumber("+977 9821577495"),
                from: new Twilio.Types.PhoneNumber("+19472827953"),
                to: new Twilio.Types.PhoneNumber("+977 9821577495")

            //from: new PhoneNumber("whatsapp:+19472827953"),
            //to: new Twilio.Types.PhoneNumber("whatsapp:+977 9821577495"),
            //body: "Hello world"
            );
        }
    }
}
