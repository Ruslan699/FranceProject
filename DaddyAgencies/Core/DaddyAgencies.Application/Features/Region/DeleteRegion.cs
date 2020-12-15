using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Region
{
    public class DeleteRegion : DeleteRequest, IRequest
    {
        public DeleteRegion(Guid id) : base(id)
        {

        }
    }
}
