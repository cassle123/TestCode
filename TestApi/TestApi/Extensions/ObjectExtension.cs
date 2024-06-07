using System.Diagnostics.CodeAnalysis;

namespace TestApi.Extensions;

public static class ObjectExtension
{
    [return: MaybeNull]
    public static T ConvertType<T>(this object? target)
    {
        try
        {
            if (target == null || target.GetType() == typeof(DBNull))
                return default;
            else
            {
                var returnType = typeof(T);
                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (target == null) return default;

                    returnType = Nullable.GetUnderlyingType(returnType);
                }
                return (T)Convert.ChangeType(target, returnType!);
            }
        }
        catch { return default; }
    }
}
