using System;
using System.Linq.Expressions;

namespace MachinaAurum.FunctionalDependencies
{
    public class Functions
    {
        public static Func<TIn, TOut> Combine<TIn, TMiddle, TOut>(Func<TIn, TMiddle> a, Func<TMiddle, TOut> b)
        {
            var canCombine = b.Method.GetParameters()[0].ParameterType.IsAssignableFrom(a.Method.ReturnType);

            if (canCombine == false)
            {
                throw new Exception();
            }

            var input = typeof(TIn);
            var output = typeof(TOut);

            var funcType = typeof(Func<,>).MakeGenericType(input, output);

            var parameters = Expression.Parameter(input, "x0");

            var callA = Expression.Invoke(Expression.Constant(a), parameters);
            var callB = Expression.Invoke(Expression.Constant(b), callA);

            var e = Expression.Lambda(callB, parameters);

            return (Func<TIn, TOut>)e.Compile();
        }

        public static Func<T1, T4> Combine<T1, T2, T3, T4>(Func<T1, T2> a, Func<T2, T3> b, Func<T3, T4> c)
        {
            return Combine(Combine(a, b), c);
        }
    }
}
