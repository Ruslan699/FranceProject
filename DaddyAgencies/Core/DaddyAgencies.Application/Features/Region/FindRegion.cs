using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Region
{
    //your command, will return obkect  Result<Models.Region>, you must put your namespace correcty, Not Property it's Region, change it in future
    public class FindRegion : FindRequest<Models.Region>, IRequest
    {
        public FindRegion(Guid uid) : base(uid)
        {
            
        }
    }
}
