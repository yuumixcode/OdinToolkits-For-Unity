using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.Universal;

namespace Yuumix.OdinToolkits.Core
{
    public static class OdinToolkitsEventInfoExtensions
    {
        [BilingualComment("获取事件的访问修饰符类型", "Get the access modifier type of an event")]
        public static AccessModifierType GetEventAccessModifierType(this EventInfo eventInfo)
        {
            MethodInfo addMethod = eventInfo.GetAddMethod(true);
            MethodInfo removeMethod = eventInfo.GetRemoveMethod(true);
            MethodInfo invokeMethod = eventInfo.GetRaiseMethod(true);
            if (invokeMethod != null)
            {
                return invokeMethod.GetMethodAccessModifierType();
            }

            if (addMethod != null && removeMethod != null)
            {
                return ((int)addMethod.GetMethodAccessModifierType() <= (int)removeMethod.GetMethodAccessModifierType()
                    ? addMethod
                    : removeMethod).GetMethodAccessModifierType();
            }

            if (addMethod != null)
            {
                return addMethod.GetMethodAccessModifierType();
            }

            if (removeMethod != null)
            {
                return removeMethod.GetMethodAccessModifierType();
            }

            return AccessModifierType.None;
        }

        [BilingualComment("获取事件的可读返回类型", "Get the readable return type of an event")]
        public static string GetReadableEventReturnType(this EventInfo eventInfo)
        {
            Type eventHandlerType = eventInfo.EventHandlerType;
            MethodInfo invokeMethod = eventHandlerType.GetMethod("Invoke");
            // 这里默认情况下不可能为空，因为事件类型肯定有Invoke方法
            if (invokeMethod == null)
            {
                return "Action";
            }

            ParameterInfo[] parameters = invokeMethod.GetParameters();
            List<string> paramTypes = parameters.Select(p => p.ParameterType.GetReadableTypeName()).ToList();
            if (invokeMethod.ReturnType != typeof(void))
            {
                string returnType = invokeMethod.ReturnType.GetReadableTypeName();
                if (paramTypes.Count > 0)
                {
                    return $"Func<{string.Join(", ", paramTypes)}, {returnType}>";
                }

                return $"Func<{returnType}>";
            }

            if (paramTypes.Count > 0)
            {
                return $"Action<{string.Join(", ", paramTypes)}>";
            }

            return "Action";
        }

        [BilingualComment("判断事件是否为静态事件", "Determine if an event is a static event")]
        public static bool IsStaticEvent(this EventInfo eventInfo)
        {
            MethodInfo addMethod = eventInfo.GetAddMethod(true);
            MethodInfo removeMethod = eventInfo.GetRemoveMethod(true);
            MethodInfo invokeMethod = eventInfo.GetRaiseMethod(true);
            if (invokeMethod != null)
            {
                return invokeMethod.IsStatic;
            }

            if (addMethod != null)
            {
                return addMethod.IsStatic;
            }

            if (removeMethod != null)
            {
                return removeMethod.IsStatic;
            }

            return false;
        }
    }
}
