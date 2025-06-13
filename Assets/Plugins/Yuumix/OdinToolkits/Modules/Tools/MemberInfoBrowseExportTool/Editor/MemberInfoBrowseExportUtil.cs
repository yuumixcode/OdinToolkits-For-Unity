using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

#pragma warning disable CS0067 // 事件从未使用过

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor
{
    public static class MemberInfoBrowseExportUtil
    {
        public static List<MemberDisplayItem> CreateApiDisplayItemList(List<MemberRawData> apiRawDataList)
        {
            var apiDisplayItems = apiRawDataList
                .Select(apiRawData => new MemberDisplayItem
                    { rawData = apiRawData, memberType = apiRawData.memberType.ToString() })
                .ToList();
            apiDisplayItems.Insert(0,
                new MemberDisplayItem()
                {
                    rawData = new MemberRawData()
                        { rawName = "成员名", fullSignature = "完整成员签名", declaringTypeName = "声明该成员的类" },
                    memberType = "成员类型"
                });
            return apiDisplayItems;
        }

        public static List<MemberRawData> CollectRawMethodInfo(Type targetType) =>
            CreateMemberInfoRawDataListFromMethodInfo(targetType.GetRuntimeMethods(), targetType, MemberTypes.Method);

        public static List<MemberRawData> CollectRawPropertyInfo(Type targetType)
        {
            var apiList = new List<MemberRawData>();
            foreach (var prop in targetType.GetRuntimeProperties())
            {
                apiList.AddRange(prop.GetAccessors(true).Select(methodInfo =>
                    CreateMemberRawDataFromMethodInfo(methodInfo, targetType, MemberTypes.Property, prop.Name)));
            }

            apiList.Sort(new MemberRawDataComparer());
            return apiList;
        }

        public static List<MemberRawData> CollectRawEventInfo(Type targetType)
        {
            var apiList = new List<MemberRawData>();
            foreach (var eventInfo in targetType.GetRuntimeEvents())
            {
                var methods = new[] { eventInfo.AddMethod, eventInfo.RemoveMethod, eventInfo.RaiseMethod };
                foreach (var method in methods)
                {
                    if (method != null)
                    {
                        var apiData =
                            CreateMemberRawDataFromMethodInfo(method, targetType, MemberTypes.Event, eventInfo.Name);
                        apiData.eventReturnTypeName = ReflectionUtil.GetReadableEventReturnType(eventInfo);
                        apiList.Add(apiData);
                    }
                }
            }

            apiList.Sort(new MemberRawDataComparer());
            return apiList;
        }

        public static List<MemberRawData> CollectRawFieldInfo(Type targetType)
        {
            var apiList = new List<MemberRawData>();
            foreach (var field in targetType.GetRuntimeFields()
                         .Where(f => !f.IsDefined(typeof(CompilerGeneratedAttribute))))
            {
                var apiData = new MemberRawData()
                {
                    BelongToType = targetType,
                    declaringTypeName = field.DeclaringType == targetType
                        ? targetType?.Name
                        : field.DeclaringType?.FullName,
                    memberType = MemberTypes.Field,
                    rawName = field.Name,
                    isObsolete = field.IsDefined(typeof(ObsoleteAttribute)),
                    isStatic = field.IsStatic
                };
                if (field.IsPublic)
                {
                    apiData.modifierType = AccessModifierType.Public;
                }
                else if (field.IsPrivate)
                {
                    apiData.modifierType = AccessModifierType.Private;
                }
                else if (field.IsFamily)
                {
                    apiData.modifierType = AccessModifierType.Protected;
                }
                else
                {
                    apiData.modifierType = AccessModifierType.Internal;
                }

                apiData.fullSignature =
                    $"{GetAccessModifierString(apiData.modifierType)} {GetStaticKeyword(apiData.isStatic)}{TypeUtil.GetReadableTypeName(field.FieldType)} {apiData.rawName};";
                apiList.Add(apiData);
            }

            apiList.Sort(new MemberRawDataComparer());
            return apiList;
        }

        public static List<MemberRawData> CollectRawConstructors(Type targetType)
        {
            var apiList = new List<MemberRawData>();
            foreach (var cons in targetType.GetConstructors())
            {
                var apiData = new MemberRawData()
                {
                    BelongToType = targetType,
                    rawName = cons.Name,
                    declaringTypeName = cons.DeclaringType == targetType
                        ? targetType?.Name
                        : cons.DeclaringType?.FullName,
                    isObsolete = cons.IsDefined(typeof(ObsoleteAttribute)),
                    memberType = MemberTypes.Constructor,
                    isStatic = cons.IsStatic,
                    isAbstract = cons.IsAbstract,
                    isVirtual = cons.IsVirtual,
                };
                if (cons.IsPublic)
                {
                    apiData.modifierType = AccessModifierType.Public;
                }
                else if (cons.IsPrivate)
                {
                    apiData.modifierType = AccessModifierType.Private;
                }
                else if (cons.IsFamily)
                {
                    apiData.modifierType = AccessModifierType.Protected;
                }
                else
                {
                    apiData.modifierType = AccessModifierType.Internal;
                }

                apiData.fullSignature =
                    $"{GetAccessModifierString(apiData.modifierType)} " +
                    $"{GetStaticKeyword(apiData.isStatic)}{GetAbstractOrVirtual(apiData.isAbstract, apiData.isVirtual)}{cons.Name}" +
                    $"({string.Join(", ", cons.GetParameters().Select(p => $"{TypeUtil.GetReadableTypeName(p.ParameterType)} {p.Name}"))})";
                apiList.Add(apiData);
            }

            apiList.Sort(new MemberRawDataComparer());
            return apiList;

            static string GetAbstractOrVirtual(bool isAbstract, bool isVirtual)
            {
                if (isAbstract)
                {
                    return "abstract ";
                }

                if (isVirtual)
                {
                    return "virtual ";
                }

                return "";
            }
        }

        public static List<MemberRawData> HandleMethodInfo(this List<MemberRawData> rawMethodList)
        {
            rawMethodList.RemoveAll(apiRaw => apiRaw.rawName != null &&
                                              (apiRaw.rawName.StartsWith("get_") || apiRaw.rawName.StartsWith("set_") ||
                                               apiRaw.rawName.StartsWith("add_") ||
                                               apiRaw.rawName.StartsWith("remove_")));
            return rawMethodList;
        }

        public static List<MemberRawData> HandlePropertyInfo(this List<MemberRawData> rawPropertyList) =>
            GroupAndProcessData(rawPropertyList, GetPropertySignature);

        public static List<MemberRawData> HandleEventInfo(this List<MemberRawData> rawEventList)
        {
            return GroupAndProcessData(rawEventList,
                (_, firstItem) => GetEventSignature(firstItem, firstItem.eventReturnTypeName));
        }

        static List<MemberRawData> CreateMemberInfoRawDataListFromMethodInfo(IEnumerable<MethodInfo> methods,
            Type targetType,
            MemberTypes memberType)
        {
            var apiList = methods.Select(methodInfo =>
                    CreateMemberRawDataFromMethodInfo(methodInfo, targetType, memberType, methodInfo.Name))
                .ToList();
            apiList.Sort(new MemberRawDataComparer());
            return apiList;
        }

        static MemberRawData CreateMemberRawDataFromMethodInfo(MethodInfo methodInfo, Type targetType,
            MemberTypes memberType,
            string rawName)
        {
            var apiData = new MemberRawData
            {
                BelongToType = targetType,
                rawName = rawName,
                declaringTypeName = methodInfo.DeclaringType == targetType
                    ? targetType.Name
                    : methodInfo.DeclaringType?.FullName,
                memberType = memberType,
                isStatic = methodInfo.IsStatic,
                isAbstract = methodInfo.IsAbstract,
                isVirtual = methodInfo.IsVirtual,
                isObsolete = methodInfo.IsDefined(typeof(ObsoleteAttribute)),
                modifierType = ReflectionUtil.GetMethodAccessModifierType(methodInfo),
                fullSignature = ReflectionUtil.GetFullMethodSignature(methodInfo),
            };
            if (methodInfo.GetCustomAttribute<LocalizedCommentAttribute>() != null)
            {
                apiData.Comment = methodInfo.GetCustomAttribute<LocalizedCommentAttribute>();
            }

            if (memberType == MemberTypes.Property)
            {
                apiData.propertyReturnTypeName = TypeUtil.GetReadableTypeName(methodInfo.ReturnType);
            }

            return apiData;
        }

        static List<MemberRawData> GroupAndProcessData(List<MemberRawData> rawList,
            Func<IGrouping<string, MemberRawData>, MemberRawData, string> getSignature)
        {
            var groupedData = rawList.GroupBy(data => data.rawName);
            var processedList = new List<MemberRawData>();

            foreach (var group in groupedData)
            {
                var firstItem = group.First();
                var newData = new MemberRawData
                {
                    BelongToType = firstItem.BelongToType,
                    rawName = firstItem.rawName,
                    declaringTypeName = firstItem.declaringTypeName,
                    memberType = firstItem.memberType,
                    modifierType = firstItem.modifierType,
                    isStatic = firstItem.isStatic,
                    isVirtual = firstItem.isVirtual,
                    isAbstract = firstItem.isAbstract,
                    isObsolete = firstItem.isObsolete,
                    fullSignature = getSignature(group, firstItem)
                };

                processedList.Add(newData);
            }

            processedList.Sort(new MemberRawDataComparer());
            return processedList;
        }

        static string GetPropertySignature(IGrouping<string, MemberRawData> group, MemberRawData firstItem)
        {
            var getSignature = "";
            var setSignature = "";
            var returnType = "";

            foreach (var item in group)
            {
                if (item.fullSignature.Contains("get_"))
                {
                    getSignature = GetAccessModifierString(item.modifierType);
                    returnType = firstItem.propertyReturnTypeName;
                }
                else if (item.fullSignature.Contains("set_"))
                {
                    setSignature = GetAccessModifierString(item.modifierType);
                }
            }

            return
                $"{GetAccessModifierString(firstItem.modifierType)} {GetStaticKeyword(firstItem.isStatic)}{returnType} " +
                $"{firstItem.rawName} {{ {getSignature} get; {setSignature} set; }}";
        }

        static string GetEventSignature(MemberRawData memberRaw, string eventReturnType)
        {
            var accessModifier = GetAccessModifierString(memberRaw.modifierType);
            var staticKeyword = memberRaw.isStatic ? "static " : "";
            var baseKeyword = "";
            if (memberRaw.isAbstract)
            {
                baseKeyword = "abstract ";
            }
            else if (memberRaw.isVirtual)
            {
                baseKeyword = "virtual ";
            }

            var modifiers = $"{accessModifier} {staticKeyword}{baseKeyword}";
            return $"{modifiers}event {eventReturnType} {memberRaw.rawName};";
        }

        static string GetAccessModifierString(AccessModifierType modifier)
        {
            return modifier switch
            {
                AccessModifierType.Public => "public",
                AccessModifierType.Private => "private",
                AccessModifierType.Protected => "protected",
                AccessModifierType.Internal => "internal",
                _ => ""
            };
        }

        static string GetStaticKeyword(bool isStatic) => isStatic ? "static " : "";
    }
}
