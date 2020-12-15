using System;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Documents
{
    public class GetDocumentsByProperty : GetRequest<DucumentBase>, IRequest
    {
        public Guid PropertyUid { get; }

        public GetDocumentsByProperty(Guid propertyUid)
        {
            PropertyUid = propertyUid;
        }
    }

    public class GetDocumentsByPropertyId : GetRequest<Guid>, IRequest
    {
        public Guid PropertyUid { get; }

        public GetDocumentsByPropertyId(Guid propertyUid)
        {
            PropertyUid = propertyUid;
        }
    }

    public class GetMainDocumentByProperty : UidRequest<string>, IRequest
    {
        public GetMainDocumentByProperty(Guid propertyUid)
        {
            Uid = propertyUid;
        }
    }

    public class GetMainPropertyImageId : UidRequest<Guid>, IRequest
    {
        public GetMainPropertyImageId(Guid propertyUid)
        {
            Uid = propertyUid;
        }
    }
}
