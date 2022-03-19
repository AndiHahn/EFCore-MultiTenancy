using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MultiTenancy.Discriminator.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyGlobalQueryFilter<TInterface>(
            this ModelBuilder builder,
            Expression<Func<TInterface, bool>> expression)
        {
            foreach (var entityType in builder.Model.GetEntityTypes().Select(e => e.ClrType))
            {
                if (entityType.GetInterface(typeof(TInterface).Name) != null)
                {
                    var newParam = Expression.Parameter(entityType);
                    var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam,
                        expression.Body);
                    builder.Entity(entityType)
                        .HasQueryFilter(Expression.Lambda(newbody, newParam));
                }
            }
        }
    }
}
