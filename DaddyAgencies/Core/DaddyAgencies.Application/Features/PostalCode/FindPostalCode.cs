using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.PostalCode
{
   public class FindPostalCode : FindRequest<Models.PostalCode>, IRequest
    {
        public FindPostalCode(Guid uid) : base(uid)
        {

        }
    }
}
