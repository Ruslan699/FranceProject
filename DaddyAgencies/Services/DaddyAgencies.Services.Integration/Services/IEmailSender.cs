using System.Threading.Tasks;
using DaddyAgencies.Services.Integration.Models;
using DaddyAgencies.Services.Integration.Models.Email;

namespace DaddyAgencies.Services.Integration.Services
{
    public interface IEmailSender : IService
    {
        Task<ServiceResponce> Send(MailRequest request);
    }
}
