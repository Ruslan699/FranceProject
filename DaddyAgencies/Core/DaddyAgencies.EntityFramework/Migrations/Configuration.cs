using System.Linq;
using System.Data.Entity.Migrations;
using System.Reflection;
using DaddyAgencies.Common.EntityFramework.Identity;

namespace DaddyAgencies.EntityFramework.Migrations
{
    using Common.MediatR.CustomAttributes;
    using Application.Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<DaddyAgencies.EntityFramework.ProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectDbContext context)
        {
            var type = typeof(UserRoles);
            var memInfo = type.GetFields();
            foreach (var fieldInfo in memInfo)
            {
                var attr = fieldInfo.GetCustomAttributes(typeof(ModelUidAttribute)).FirstOrDefault();
                if (attr is ModelUidAttribute modelUid)
                {
                    var val = fieldInfo.Name;
                    var uid = modelUid.ModelUid;
                    context.Roles.AddOrUpdate(new IdentityRole(uid, val));
                }
            }
        }
    }
}
