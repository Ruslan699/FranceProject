using System;

namespace DaddyAgencies.Common.MediatR.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ModelUidAttribute : Attribute
    {
        public Guid ModelUid { get;}

        public ModelUidAttribute(string modelUid)
        {
            ModelUid  = Guid.Parse(modelUid);
        }
    }
}
