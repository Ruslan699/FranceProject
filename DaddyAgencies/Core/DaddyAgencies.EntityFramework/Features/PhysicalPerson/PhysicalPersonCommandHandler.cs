using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.EntityFramework.Models;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.PhysicalPerson
{
    using ApplicationFeatures = Application.Features.PhysicalPerson;
    using ApplicationModels = Application.Models;

    public class PhysicalPersonCommandHandler : RequestHandler<ApplicationModels.PhysicalPerson, PhysicalPersonEntity>,
        IRequestHandler<ApplicationFeatures.FindPhysicalPersonByUser, Result<ApplicationModels.PhysicalPerson>>,
        IRequestHandler<ApplicationFeatures.SavePhysicalPersonDraft, Result<Guid>>
    {
        public PhysicalPersonCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<Result<ApplicationModels.PhysicalPerson>> Handle(
            ApplicationFeatures.FindPhysicalPersonByUser request, CancellationToken cancellationToken) =>
            HandleFindByRequest(request, p => p.UserUid == request.UserId, cancellationToken);

        public Task<Result<Guid>> Handle(ApplicationFeatures.SavePhysicalPersonDraft request,
            CancellationToken cancellationToken)
            => HandleSaveRequest(request, cancellationToken);
    }
}
