using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;

namespace Yuumix.OdinToolkits.Samples.TestMultiLanguageButtonAttribute
{
    public class SampleMultiLanguageButtonAttribute : MonoBehaviour
    {
        public SwitchInspectorLanguageWidget switchInspectorLanguageWidget;

        [MultiLanguageButton("$Chinese", "Test")]
        void Button()
        {
            Debug.Log("测试 MultiLanguageButton");
        }

        string Chinese()
        {
            return "LILLLIE";
        }

        string Lan()
        {
            return InspectorMultiLanguageManagerSO.IsChinese ? "测试" : "Test";
        }
    }
}
