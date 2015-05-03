using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sample.Core.Common
{
    public static class LinqExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            return source;
        }

        public static T? GetValueOrNull<T>(this string valueAsString)
            where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }

        public static string GetExpressionName(MemberExpression memberExpression)
        {
            if (memberExpression.Expression.NodeType == ExpressionType.Parameter)
                return memberExpression.Member.Name;

            if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                return string.Format("{0}.{1}", GetExpressionName(memberExpression.Expression as MemberExpression), memberExpression.Member.Name);

            var methodCallExpression = (MethodCallExpression)memberExpression.Expression;
            if (methodCallExpression.Arguments.Count != 1)
                throw new Exception("invalid method call in Include expression");
            return string.Format("{0}.{1}", GetExpressionName(methodCallExpression.Arguments[0] as MemberExpression), memberExpression.Member.Name);
        }

        public static IEnumerable<T> FlattenHierarchy<T>(this T node, Func<T, IEnumerable<T>> getChildEnumerator)
        {
            yield return node;
            if (getChildEnumerator(node) != null)
            {
                foreach (var child in getChildEnumerator(node))
                {
                    foreach (var childOrDescendant in child.FlattenHierarchy(getChildEnumerator))
                    {
                        yield return childOrDescendant;
                    }
                }
            }
        }

        public static string GetPropertyName<TSource, TField>(Expression<Func<TSource, TField>> Field)
        {
            return (Field.Body as MemberExpression ?? ((UnaryExpression)Field.Body).Operand as MemberExpression).Member.Name;
        }
    }
}