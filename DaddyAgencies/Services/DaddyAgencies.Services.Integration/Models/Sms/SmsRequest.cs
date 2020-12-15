
namespace DaddyAgencies.Services.Integration.Models.Sms
{
    public class SmsRequest : RequestBase
    {
        public string NumberTo { get; set; }

        public string SmsBody { get; set; }
    }
}
