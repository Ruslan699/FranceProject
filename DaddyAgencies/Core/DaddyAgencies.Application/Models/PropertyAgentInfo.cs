using System;

namespace DaddyAgencies.Application.Models
{
    public class PropertyAgentInfo
    {
        public Guid? PropertyUid { get; set; }
        public string PropertyName { get; set; }
        public string UserEmail { get; set; }
        public Guid? UserUid { get; set; }
    }
}
