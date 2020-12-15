using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DaddyAgencies.Common.EntityFramework.Identity;
using DaddyAgencies.EntityFramework.Models;

namespace DaddyAgencies.EntityFramework
{
    public class ProjectDbContext : IdentityDbContext
    {
        public ProjectDbContext() : base("ProjectConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RegionEntity> RegionEntities { get; set; }
        public DbSet<DepartamentEntity> DepartamentEntities { get; set; }
        public DbSet<PostalCodeEntity> PostalCodeEntities { get; set; }


        public DbSet<PropertyEntity> PropertyEntities { get; set; }
        public DbSet<PropertyDocumentEntity> PropertyDocumentEntities { get; set; }

        public DbSet<PropertyFloorTypeEntity> PropertyFloorTypeEntities { get; set; }
        public DbSet<PropertyFloorEntity> PropertyFloorEntities { get; set; }

        public DbSet<AdditionalElementEntity> AdditionalElementEntities { get; set; }
        public DbSet<PropertyAdditionalElementEntity> PropertyAdditionalElementEntities { get; set; }


        public DbSet<InseptionEntity> InseptionEntities { get; set; }
        public DbSet<RecruitmentEntity> RecruitmentEntities { get; set; }
        public DbSet<RecruitmentDocumentEntity> RecruitmentDocumentEntities { get; set; }
        public DbSet<PhysicalPersonEntity> PhysicalPersonEntities { get; set; }
        public DbSet<UserPostalCodeEntity> UserPostalCodeEntities { get; set; }

        
    }
}
