
using System;

namespace DaddyAgencies.Services.Integration.Models
{
    public class ServiceResponce
    {
        public ServiceResponce(Guid requstUid, bool isSuccess, string message)
        {
            Uid = Guid.NewGuid();
            RequstUid = requstUid;
            IsSuccess = isSuccess;
            Message = message;
        }

        public Guid Uid { get; set; }

        public Guid RequstUid { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
