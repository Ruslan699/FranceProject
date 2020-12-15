using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataAccess.CustomAttributes;

namespace DataAccess.Context
{
    internal class DataContext : DbContext
    {
        public DataContext() : base("ProjectConnectionString")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetDecimalPrecision(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void SetDecimalPrecision(DbModelBuilder modelBuilder, Assembly assembly = null, string calledNamespace = null)
        {
            if (assembly == null || calledNamespace == null)
            {
                var stackTrace = new StackTrace(1);
                var stackFrame = stackTrace.GetFrame(0);
                var declaringType = stackFrame.GetMethod().DeclaringType;

                if (calledNamespace == null)
                    calledNamespace = declaringType?.Namespace;

                if (assembly == null)
                    assembly = declaringType?.Assembly;

                if (calledNamespace == null || assembly == null)
                    return;
            }

            foreach (var classType in from t in assembly.GetTypes()
                                      where t.IsClass && t.Namespace == calledNamespace
                                      select t)
            {
                foreach (var propAttr in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null).Select(
                       p => new { prop = p, attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(true) }))
                {

                    var entityConfig = modelBuilder?.GetType()?.GetMethod("Entity")?.MakeGenericMethod(classType).Invoke(modelBuilder, null);
                    if (entityConfig == null)
                        continue;

                    var param = Expression.Parameter(classType, "c");
                    var property = Expression.Property(param, propAttr.prop.Name);
                    var lambdaExpression = Expression.Lambda(property, true, param);
                    DecimalPropertyConfiguration decimalConfig;
                    if (propAttr.prop.PropertyType.IsGenericType && propAttr.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[7];
                        decimalConfig = methodInfo.Invoke(entityConfig, new object[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }
                    else
                    {
                        var methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[6];
                        decimalConfig = methodInfo.Invoke(entityConfig, new object[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }

                    decimalConfig?.HasPrecision(propAttr.attr.Precision, propAttr.attr.Scale);
                }
            }
        }

        public DbSet<RegionEntity> RegionEntities { get; set; }
    }
}
