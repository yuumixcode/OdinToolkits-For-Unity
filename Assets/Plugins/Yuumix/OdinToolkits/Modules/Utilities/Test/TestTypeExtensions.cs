using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;
using TypeExtensions = Yuumix.OdinToolkits.Modules.Utilities.Runtime.TypeExtensions;

namespace Yuumix.OdinToolkits.Modules.Utilities.Test
{
    [Serializable]
    public class ForTestTypeUtilGenericNestedClass<TCollection, TItem>
        where TCollection : IEnumerable<TItem> { }

    [TypeInfoBox("如果 testData 存在，则优先使用 testData")]
    public class TestTypeExtensions : SerializedMonoBehaviour
    {
        [LocalizedTitle("测试数据", "TestData")]
        [HideLabel]
        public TestTypeUtilDataSO testData;

        public List<Type> TypesForTest;

        [MultiLineProperty(Lines = 3)]
        public List<string> returnList;

        [Button("测试 TypeUtil.GetTypeDeclaration()", ButtonSizes.Large)]
        public void TestGetTypeDeclaration()
        {
            if (testData)
            {
                returnList = testData.TypeListForTest.Select(TypeExtensions.GetTypeDeclaration)
                    .ToList();
            }
            else
            {
                returnList = TypesForTest.Select(TypeExtensions.GetTypeDeclaration)
                    .ToList();
            }
        }

        [Button("测试 TypeUtil.GetReadableTypeName()", ButtonSizes.Large)]
        public void TestGetReadableTypeName()
        {
            if (testData)
            {
                returnList = testData.TypeListForTest.Select(x => x.GetReadableTypeName())
                    .ToList();
            }
            else
            {
                returnList = TypesForTest.Select(x => x.GetReadableTypeName())
                    .ToList();
            }
        }

        [Button("测试 TypeExtensions.GetNiceName()", ButtonSizes.Large)]
        public void TestCreateNiceName()
        {
            if (testData)
            {
                returnList = testData.TypeListForTest.Select(x => x.GetNiceName())
                    .ToList();
            }
        }
    }
}
