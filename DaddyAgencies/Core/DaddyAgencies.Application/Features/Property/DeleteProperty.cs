﻿using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Property
{
   public class DeleteProperty : DeleteRequest, IRequest
    {
        public DeleteProperty(Guid id) : base(id)
        {
        }
    }
}
