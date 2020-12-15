using System;

namespace DaddyAgencies.Services.Integration.Models
{
    public abstract class RequestBase
    {
        public Guid Uid { get; set; }

        protected RequestBase() => Uid = Guid.NewGuid();

        public ServiceResponce SuccessResponce(string message = "") => new ServiceResponce(Uid, true, message);
    }
}
