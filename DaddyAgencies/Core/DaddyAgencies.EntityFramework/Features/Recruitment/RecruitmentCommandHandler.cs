using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Application.Features.Recruiment;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.EntityFramework.Models;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.Recruitment
{
    public class RecruitmentCommandHandler : RequestHandler<SaveRecruitment, RecruitmentEntity>, 
        IRequestHandler<SaveRecruitment, Result<Guid>>
    {
        public RecruitmentCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<Result<Guid>> Handle(SaveRecruitment request, CancellationToken cancellationToken) 
            => HandleSaveRequest(request, cancellationToken);
    }
}
