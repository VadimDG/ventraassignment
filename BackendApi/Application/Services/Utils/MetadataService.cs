using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Utils
{
    public class MetadataService
    {
        public static object Metadata<T>()
        {
            var baseType = typeof(T);

            var properties = baseType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Select(p =>
                {
                    var accessor = p.GetMethod ?? p.SetMethod;
                    var propType = p.PropertyType;
                    var referenced = GetReferencedType(propType);

                    var subMembers = new List<object>();
                    if (referenced != null && !IsSimpleType(referenced) && !IsSystemAssembly(referenced.Assembly))
                    {
                        subMembers.AddRange(GetMembersOfType(referenced));
                    }

                    return new
                    {
                        p.Name,
                        Type = GetFriendlyTypeName(propType),
                        Kind = "Property",
                        IsStatic = accessor != null && accessor.IsStatic,
                        IsEditable = GetIsEditableForMember(p, propType),
                        SubMembers = subMembers
                    };
                })
                .ToList();

            var baseTypeInfo = new
            {
                baseType.Name,
                baseType.Namespace,
                Assembly = baseType.Assembly.GetName().Name,
                baseType.IsAbstract,
                baseType.IsInterface,
                PublicPropertyCount = properties.Count
            };

            return new
            {
                BaseType = baseTypeInfo,
                Properties = properties
            };
        }

        private static IEnumerable<object> GetMembersOfType(Type type)
        {
            var fields = type
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Select(f => new
                {
                    f.Name,
                    Type = GetFriendlyTypeName(f.FieldType),
                    Kind = "Field",
                    f.IsStatic,
                    IsEditable = GetIsEditableForMember(f, f.FieldType)
                });

            var props = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Select(p =>
                {
                    var accessor = p.GetMethod ?? p.SetMethod;
                    return new
                    {
                        p.Name,
                        Type = GetFriendlyTypeName(p.PropertyType),
                        Kind = "Property",
                        IsStatic = accessor != null && accessor.IsStatic,
                        IsEditable = GetIsEditableForMember(p, p.PropertyType)
                    };
                });

            return fields.Cast<object>().Concat(props.Cast<object>());
        }

        private static Type? GetReferencedType(Type type)
        {
            if (type == null) return null;

            if (Nullable.GetUnderlyingType(type) is Type underlying)
                return underlying;

            if (type.IsArray)
                return type.GetElementType();

            if (type.IsGenericType)
            {
                var args = type.GetGenericArguments();
                if (args.Length == 1)
                    return args[0];
            }

            return type;
        }

        private static bool GetIsEditableForMember(MemberInfo member, Type memberType)
        {
            var underlying = Nullable.GetUnderlyingType(memberType) ?? memberType;

            if (!(underlying.IsValueType || underlying == typeof(string)))
                return false;

            var editableAttr = member.GetCustomAttributes(typeof(EditableAttribute), inherit: true)
                                     .OfType<EditableAttribute>()
                                     .FirstOrDefault();

            return editableAttr?.AllowEdit ?? false;
        }

        private static bool IsSimpleType(Type type)
        {
            if (type.IsPrimitive) return true;
            if (type.IsEnum) return true;
            if (type == typeof(string)) return true;
            if (type == typeof(decimal)) return true;
            if (type == typeof(DateTime)) return true;
            if (type == typeof(DateTimeOffset)) return true;
            if (type == typeof(Guid)) return true;
            if (type == typeof(TimeSpan)) return true;
            if (type == typeof(Uri)) return true;
            if (type.IsValueType) return true;
            return false;
        }

        private static bool IsSystemAssembly(Assembly? a)
        {
            if (a == null) return true;
            var name = a.GetName().Name ?? string.Empty;
            return name.StartsWith("System", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase)
                || name.Equals("mscorlib", StringComparison.OrdinalIgnoreCase)
                || name.Equals("netstandard", StringComparison.OrdinalIgnoreCase);
        }

        protected static string GetFriendlyTypeName(Type type)
        {
            if (type == null) return "unknown";

            var keyword = type == typeof(int) ? "int"
                       : type == typeof(long) ? "long"
                       : type == typeof(short) ? "short"
                       : type == typeof(byte) ? "byte"
                       : type == typeof(bool) ? "bool"
                       : type == typeof(string) ? "string"
                       : type == typeof(decimal) ? "decimal"
                       : type == typeof(double) ? "double"
                       : null;

            if (keyword != null) return keyword;

            if (type.IsArray)
            {
                return $"{GetFriendlyTypeName(type.GetElementType()!)}[]";
            }

            if (Nullable.GetUnderlyingType(type) is Type underlying)
            {
                return GetFriendlyTypeName(underlying) + "?";
            }

            if (type.IsGenericType)
            {
                var genDef = type.GetGenericTypeDefinition();
                var genArgs = type.GetGenericArguments().Select(GetFriendlyTypeName);
                var genName = genDef.Name.Split('`')[0];

                if (genDef == typeof(IEnumerable<>)) genName = "IEnumerable";
                if (genDef == typeof(List<>)) genName = "List";
                if (genDef == typeof(Dictionary<,>)) genName = "Dictionary";

                return $"{genName}<{string.Join(", ", genArgs)}>";
            }

            return type.Name;
        }
    }
}
