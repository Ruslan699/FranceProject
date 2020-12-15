using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Region
{
    public class SaveRegion : SaveRequest, IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathString { get; set; }

    }
}
