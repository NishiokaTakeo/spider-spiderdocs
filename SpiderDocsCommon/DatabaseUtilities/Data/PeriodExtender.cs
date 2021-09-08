using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;


//---------------------------------------------------------------------------------
namespace Spider.Data
{
    public static class PeriodExtender
    {
        /// <summary>
        /// <para>This is an extend method for Linq.</para>
        /// <para>It checks if given DateTime is in period of "criteria" variable or not.</para>
        /// </summary>
        public static IQueryable<T> InThisPerios<T>(this IQueryable<T> source, Expression<Func<T, DateTime>> keySelector, Period criteria)
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());

            Expression condition;

            condition = Expression.AndAlso(Expression.NotEqual(key, Expression.Constant(new DateTime())),
                            Expression.Or(
                                Expression.NotEqual(Expression.Constant(criteria.From), Expression.Constant(new DateTime())),
                                Expression.NotEqual(Expression.Constant(criteria.To), Expression.Constant(new DateTime()))));

            condition = Expression.AndAlso(condition,
                            Expression.Or(
                                Expression.Equal(Expression.Constant(criteria.From), Expression.Constant(new DateTime())),
                                Expression.LessThanOrEqual(Expression.Constant(criteria.From), key)));

            condition = Expression.AndAlso(condition,
                            Expression.Or(
                                Expression.Equal(Expression.Constant(criteria.To), Expression.Constant(new DateTime())),
                                Expression.GreaterThanOrEqual(Expression.Constant(criteria.To), key)));

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(condition, keySelector.Parameters);
            return source.AsExpandable().Where(lambda);
        }
    }
}
