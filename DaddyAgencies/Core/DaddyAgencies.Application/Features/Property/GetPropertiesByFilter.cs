using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.Application.Features.Property
{
    public class GetPropertiesByFilter : GetRequest<PropertyPreview>, IRequest
    {
        public PropertyFilter Filter { get; set; }

        public GetPropertiesByFilter(bool includeInactive = false) : base(includeInactive)
        {

        }
    }
    public class GetPropertyCountByFilter : Request<int>, IRequest
    {
        public PropertyFilter Filter { get; set; }

        public GetPropertyCountByFilter()
        {

        }

        public GetPropertyCountByFilter(PropertyFilter filter)
        {
            Filter = filter;
        }
    }
}