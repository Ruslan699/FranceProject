using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Documents
{
    public class DeleteDocument : DeleteRequest, IRequest
    {
        public DeleteDocument(Guid id): base(id)
        {

        }
    }
}
