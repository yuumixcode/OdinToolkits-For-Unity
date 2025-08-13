using System.Reflection;

namespace Yuumix.Universal
{
    public static class FieldInfoExtensions
    {
        [BilingualComment("判断字段是否为常量字段", "Determine if a field is a constant field")]
        public static bool IsConstantField(this FieldInfo field) => field.IsLiteral && !field.IsInitOnly;

        [BilingualComment("获取字段的访问修饰符类型", "Get the access modifier type of a field")]
        public static AccessModifierType GetFieldAccessModifierType(this FieldInfo field)
        {
            if (field.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (field.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            if (field.IsFamily)
            {
                return AccessModifierType.Protected;
            }

            if (field.IsAssembly)
            {
                return AccessModifierType.Internal;
            }

            if (field.IsFamilyOrAssembly)
            {
                return AccessModifierType.ProtectedInternal;
            }

            if (field.IsFamilyAndAssembly)
            {
                return AccessModifierType.PrivateProtected;
            }

            return AccessModifierType.None;
        }
    }
}
