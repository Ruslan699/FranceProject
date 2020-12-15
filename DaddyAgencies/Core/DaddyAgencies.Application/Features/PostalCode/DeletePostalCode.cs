using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.PostalCode
{
   public class DeletePostalCode : DeleteRequest, IRequest
    {
        public DeletePostalCode(Guid id) : base(id)
        {
        }
    }
}
