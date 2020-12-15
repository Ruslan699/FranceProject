
namespace DaddyAgencies.Services.Integration.Models.Email
{
    public class MailRequest : RequestBase
    {
        public string AddressTo { get; set; }

        public string MailTitle { get; set; }

        public string MailBody { get; set; }
    }
}
