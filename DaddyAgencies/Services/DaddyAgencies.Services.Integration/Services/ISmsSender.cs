
using System.Threading.Tasks;
using DaddyAgencies.Services.Integration.Models;
using DaddyAgencies.Services.Integration.Models.Sms;

namespace DaddyAgencies.Services.Integration.Services
{
    public interface ISmsSender : IService
    {
        Task<ServiceResponce> Send(SmsRequest request);
    }
}
