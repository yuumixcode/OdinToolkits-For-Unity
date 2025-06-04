using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.APIBrowser.Tests
{
    public static class ApiBrowserTestCommands
    {
        static void TestHandleEvent()
        {
            var rawEventList = ApiBrowserUtil.CollectRawEventInfo(typeof(EventClassTestBase));
            Debug.Log("--- 原始事件数据 ---");
            foreach (var raw in rawEventList)
            {
                Debug.Log(raw.rawName);
                Debug.Log(raw.fullSignature);
            }

            rawEventList = rawEventList.HandleEventInfo();
            Debug.Log(">>> 处理后的事件数据 >>>");
            foreach (var data in rawEventList)
            {
                Debug.Log(data.fullSignature);
            }
        }

        static void TestHandleProperty()
        {
            var rawPropertyList = ApiBrowserUtil.CollectRawPropertyInfo(typeof(PropertyClassTestBase));
            Debug.Log("--- 原始属性数据 ---");
            foreach (var raw in rawPropertyList)
            {
                Debug.Log(raw.modifierType);
                Debug.Log(raw.fullSignature);
            }

            var processedPropertyList = rawPropertyList.HandlePropertyInfo();
            Debug.Log(">>> 处理后的属性数据 >>>");
            foreach (var data in processedPropertyList)
            {
                Debug.Log(data.fullSignature);
            }
        }

        static void TestHandleField()
        {
            var rawFieldList = ApiBrowserUtil.CollectRawFieldInfo(typeof(FieldClassTestBase));
            Debug.Log("--- 原始字段数据 ---");
            foreach (var raw in rawFieldList)
            {
                Debug.Log(raw.modifierType);
                Debug.Log(raw.fullSignature);
            }
        }
    }
}