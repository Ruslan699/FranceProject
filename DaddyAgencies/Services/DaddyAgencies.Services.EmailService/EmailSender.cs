using DaddyAgencies.Services.Integration.Services;
using System.Threading.Tasks;
using DaddyAgencies.Services.Integration;
using DaddyAgencies.Services.Integration.Models;
using DaddyAgencies.Services.Integration.Models.Email;

namespace DaddyAgencies.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public ServiceType ServiceType => ServiceType.Email;

        public Task<ServiceResponce> Send(MailRequest request) => Task.FromResult(request.SuccessResponce());
    }
}
