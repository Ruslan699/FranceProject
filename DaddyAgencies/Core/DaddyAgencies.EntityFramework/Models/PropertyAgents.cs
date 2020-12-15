using System;

namespace DaddyAgencies.EntityFramework.Models
{
    public class PropertyAgents
    {
        public Guid? PropertyUid { get; set; }
        public string PropertyName { get; set; }
        public string UserEmail { get; set; }
        public Guid UserUid { get; set; }
    }
}
