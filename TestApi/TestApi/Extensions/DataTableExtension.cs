using System.Data;
using System.Reflection;

namespace TestApi.Extensions;

public static class DataTableExtension
{
    public static List<T> ToClass<T>(this DataTable table) where T : new()
    {
        List<T> list = new List<T>();
        List<PropertyInfo> propInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<IgnoreGenerateAttribute>() == null)
            .ToList();

        Dictionary<int, string> indexedPropName = new Dictionary<int, string>();
        for (int i = 0; i < table.Columns.Count; i++)
            if (propInfos.Find(x => x.Name == table.Columns[i].ColumnName) != null)
                indexedPropName[i] = table.Columns[i].ColumnName;
        if (indexedPropName.Count == 0) return list;
        foreach (DataRow row in table.Rows)
        {
            T item = new T();
            foreach (var pair in indexedPropName)
                TrySetProperty(item, pair.Value, row[pair.Key]);
            list.Add(item);
        }
        return list;
    }

    public static bool TrySetProperty(object obj, string propertyName, object? value)
    {
        MemberInfo? method = typeof(ObjectExtension).GetMethod(nameof(ObjectExtension.ConvertType), BindingFlags.Public | BindingFlags.Static);
        var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (property != null && property.CanWrite)
        {
            Type type = property.PropertyType;
            if (type == value?.GetType())
                property.SetValue(obj, value, null);
            else
            {
                if (type == typeof(DateTime?))
                {
                    //DateTime? dateTime = 
                }
                else
                {

                }
            }

            return true;
        }
        return false;
    }
}

public class IgnoreGenerateAttribute : Attribute
{
    public IgnoreGenerateAttribute() { }
}