using System.Reflection;

namespace Yuumix.Universal
{
    public static class PropertyInfoExtensions
    {
        [BilingualComment("获取属性的访问修饰符类型", "Get the access modifier type of a property")]
        public static AccessModifierType GetPropertyAccessModifierType(this PropertyInfo property)
        {
            MethodInfo getMethod = property.GetGetMethod(true);
            MethodInfo setMethod = property.GetSetMethod(true);

            AccessModifierType? getAccess = getMethod != null ? getMethod.GetMethodAccessModifierType() : null;
            AccessModifierType? setAccess = setMethod != null ? setMethod.GetMethodAccessModifierType() : null;

            if (!getAccess.HasValue && !setAccess.HasValue)
            {
                return AccessModifierType.None;
            }

            if (!setAccess.HasValue)
            {
                return getAccess.Value;
            }

            if (!getAccess.HasValue)
            {
                return setAccess.Value;
            }

            return (int)getAccess.Value <= (int)setAccess.Value
                ? getAccess.Value
                : setAccess.Value;
        }
    }
}
