using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class DeleteInseption : DeleteRequest, IRequest
    {
        public DeleteInseption(Guid id) : base(id)
        {

        }
    }
}
