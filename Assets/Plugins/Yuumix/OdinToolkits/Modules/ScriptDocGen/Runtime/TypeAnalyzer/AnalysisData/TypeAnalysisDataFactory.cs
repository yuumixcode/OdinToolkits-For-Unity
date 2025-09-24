using System;
using System.Linq;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public class TypeAnalysisDataFactory : ITypeAnalysisDataFactory
    {
        /// <summary>
        /// 生成类型分析数据
        /// </summary>
        public TypeAnalysisData CreateFromType(Type type)
        {
            var constructorFactory = new ConstructorAnalysisDataFactory();
            var methodFactory = new MethodAnalysisDataFactory();
            var eventFactory = new EventAnalysisDataFactory();
            var propertyFactory = new PropertyAnalysisDataFactory();
            var fieldFactory = new FieldAnalysisDataFactory();

            return new TypeAnalysisData
            {
                typeCategory = type.GetTypeCategory(),
                assemblyName = type.Assembly.GetName().Name,
                namespaceName = type.Namespace,
                typeDeclaration = type.GetTypeDeclaration(),
                typeName = type.GetReadableTypeName(),
                chineseDescription = type.GetChineseDescription(),
                englishDescription = type.GetEnglishDescription(),
                isGeneric = type.IsGenericType,
                isNested = type.IsNested,
                isStatic = type.IsStatic(),
                isSealed = type.IsSealed,
                isAbstract = type.IsAbstract,
                isObsolete = type.IsDefined(typeof(ObsoleteAttribute), false),
                referenceWebLinkArray = type.GetReferenceLinks(),
                inheritanceChain = type.GetInheritanceChain(),
                interfaceArray = type.GetInterfacesArray(),
                constructorAnalysisDataArray = type.CreateAllPublicConstructorAnalysisDataArray(),
                methodAnalysisDataArray = MarkOverloadMethod(type.CreateRuntimeMethodAnalysisDataArray()),
                eventAnalysisDataArray = type.CreateRuntimeEventAnalysisDataArray(),
                propertyAnalysisDataArray = type.CreateRuntimePropertyAnalysisDataArray(),
                fieldAnalysisDataArray = type.CreateUserDefinedFieldAnalysisDataArray()
            };
        }

        /// <summary>
        /// 标记类型中存在的重载方法，加上 [Overload] 前缀
        /// </summary>
        public MethodAnalysisData[] MarkOverloadMethod(MethodAnalysisData[] methodAnalysisDataArray)
        {
            const string overloadPrefix = "[Overload] ";
            for (var i = 0; i < methodAnalysisDataArray.Length; i++)
            {
                for (var j = 0; j < methodAnalysisDataArray.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (methodAnalysisDataArray[i].partSignature == methodAnalysisDataArray[j].partSignature)
                    {
                        methodAnalysisDataArray[i].partSignature = methodAnalysisDataArray[i].partSignature
                            .EnsureStartsWith(overloadPrefix);
                        methodAnalysisDataArray[j].partSignature = methodAnalysisDataArray[i].partSignature
                            .EnsureStartsWith(overloadPrefix);
                        methodAnalysisDataArray[i].isOverloadMethodInDeclaringType = true;
                        methodAnalysisDataArray[j].isOverloadMethodInDeclaringType = true;
                    }
                }
            }

            return methodAnalysisDataArray;
        }
    }
}
