using AutoMapper;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.EntityFramework.Identity;
using DaddyAgencies.Common.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DaddyAgencies.EntityFramework.Models;

namespace DaddyAgencies.EntityFramework.Features.Users
{
    using ApplicationFeatures = Application.Features.User;
    using ApplicationModels = Application.Models;

    public class UserCommandHandler : RequestHandler,
        IRequestHandler<ApplicationFeatures.GetUsers, Result<IQueryable<ApplicationModels.User>>>,
        IRequestHandler<ApplicationFeatures.FindUser, Result<ApplicationModels.User>>,
        IRequestHandler<ApplicationFeatures.SaveUserPostalCodes, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.DeleteUserReferences, Result>
    {

        public UserCommandHandler(IMapper mapper) : base(mapper)
        {
            //using (var cnx = new ProjectDbContext())
            //{
            //    var userEntities = cnx.Users.ToArray();
            //}
        }

        

        public Task<Result<IQueryable<ApplicationModels.User>>> Handle(ApplicationFeatures.GetUsers request, CancellationToken cancellationToken)
        {
                var users = new ProjectDbContext().Users.ProjectToQueryable<ApplicationModels.User>(Mapper.ConfigurationProvider);
                return Task.FromResult(request.AsResultOf(users));
        }

        //public async Task<Result<User>> Handle(ApplicationFeatures.FindUser request, CancellationToken cancellationToken)
        //{
        //    Result<IdentityUser> model;
        //    using (var db = new ProjectDbContext())
        //    {
        //        model = await db.Set<ApplicationFeatures.FindUser>()
        //            .AsNoTracking()
        //            .ProjectToFirstOrDefaultAsync<IdentityUser, ApplicationFeatures.FindUser>(findBy, Mapper.ConfigurationProvider)
        //            .Map(request.AsResultOf);
        //    }
        //    return model;
        //}



        //public Task<Result<ApplicationModels.User>> Handle(ApplicationFeatures.FindUser request,
        //     CancellationToken cancellationToken) => HandleFindRequest(request, cancellationToken);

        public async Task<Result<Guid>> Handle(ApplicationFeatures.SaveUser request, CancellationToken cancellationToken)
        {
            IdentityUser dbEntity;
            using (var db = new ProjectDbContext())
            {
                if (request.Uid == default(Guid))
                {
                    dbEntity = Mapper.Map<IdentityUser>(request);
                    db.Set<IdentityUser>().Add(dbEntity);
                }
                else
                {
                    dbEntity = await db.Set<IdentityUser>().FirstAsync(x => x.Id == request.Uid, cancellationToken);
                    Mapper.Map(request, dbEntity);
                }
                await db.SaveChangesAsync(cancellationToken);
            }

            return request.AsResultOf(dbEntity.Id);
        }

        public Task<Result<ApplicationModels.User>> Handle(ApplicationFeatures.FindUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Guid>> Handle(ApplicationFeatures.SaveUserPostalCodes request, CancellationToken cancellationToken)
        {
            using (var db = new ProjectDbContext())
            {
                var usePostals = await db.UserPostalCodeEntities.Where(x => x.UserUid == request.UserId).ToListAsync(cancellationToken);

                var originalUsePostals = new List<UserPostalCodeEntity>(usePostals);

                foreach (var userPostalCodeEntity in originalUsePostals)
                {
                    db.Entry(userPostalCodeEntity).State = EntityState.Deleted;
                }


                foreach (var postalCode in request.PostalCodes)
                {
                    db.UserPostalCodeEntities.Add(new UserPostalCodeEntity()
                    {
                        UserUid = request.UserId,
                        PostalCodeUid = postalCode,
                        Uid = Guid.NewGuid(),
                        CreatedUtc = DateTime.Now,
                        IsDeleted = false
                    });
                }

                await db.SaveChangesAsync(cancellationToken);
            }

            return request.AsResultOf(request.Id);
        }

        public async Task<Result> Handle(ApplicationFeatures.DeleteUserReferences request, CancellationToken cancellationToken)
        {
            var adminMail = ConfigurationManager.AppSettings["AdminEmail"];
            using (var db = new ProjectDbContext())
            {
                var adminUid = await db.Users.Where(x => x.Email == adminMail).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
                if (adminUid == default(Guid))
                    throw new ArgumentOutOfRangeException(nameof(adminUid), @"Can't find user for given email");

                var physicalPersons = db.PhysicalPersonEntities.Where(x => x.UserUid == request.UserId);
                var postalCodes = db.UserPostalCodeEntities.Where(x => x.UserUid == request.UserId);

                var originalPhysicalPersons = new List<PhysicalPersonEntity>(physicalPersons);
                foreach (var entity in originalPhysicalPersons)
                    db.Entry(entity).State = EntityState.Deleted;

                var originalPostalCodes = new List<UserPostalCodeEntity>(postalCodes);
                foreach (var entity in originalPostalCodes)
                    db.Entry(entity).State = EntityState.Deleted;

                var inseptions = await db.InseptionEntities
                    .Where(x => x.RequesterUserUid == request.UserId || x.ConfirmerUserUid == request.UserId)
                    .ToArrayAsync(cancellationToken);

                foreach (var inseption in inseptions)
                {
                    if (inseption.ConfirmerUserUid.HasValue && inseption.ConfirmerUserUid.Value == request.UserId)
                        inseption.ConfirmerUserUid = adminUid;

                    if (inseption.RequesterUserUid.HasValue && inseption.RequesterUserUid.Value == request.UserId)
                        inseption.RequesterUserUid = adminUid;
                }

                await db.SaveChangesAsync(cancellationToken);
            }

            return request.AsResult();
        }
    }
}
