using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DaddyAgencies.Common.EntityFramework.CustomAttributes;

namespace DaddyAgencies.Common.EntityFramework
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext()
            : this("DefaultConnection")
        {
        }

        protected BaseDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected BaseDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected BaseDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        protected BaseDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        protected BaseDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetDecimalPrecision(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected void SetDecimalPrecision(DbModelBuilder modelBuilder, Assembly assembly = null, string calledNamespace = null)
        {
            if (assembly == null)
                assembly = GetType().Assembly;
            if (string.IsNullOrWhiteSpace(calledNamespace))
                calledNamespace = GetType().Namespace;

            foreach (var classType in from t in assembly.GetTypes()
                                      where t.Namespace != null && (t.IsClass && t.Namespace.StartsWith(calledNamespace ?? throw new ArgumentNullException(nameof(calledNamespace))))
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

    }
}
