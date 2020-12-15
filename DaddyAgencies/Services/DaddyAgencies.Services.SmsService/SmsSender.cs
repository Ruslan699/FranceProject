using System.Threading.Tasks;
using DaddyAgencies.Services.Integration;
using DaddyAgencies.Services.Integration.Models;
using DaddyAgencies.Services.Integration.Models.Sms;
using DaddyAgencies.Services.Integration.Services;

namespace DaddyAgencies.Services.SmsService
{
    public class SmsSender : ISmsSender
    {
        public ServiceType ServiceType => ServiceType.Sms;

        public Task<ServiceResponce> Send(SmsRequest request) => Task.FromResult(request.SuccessResponce());
    }
}
