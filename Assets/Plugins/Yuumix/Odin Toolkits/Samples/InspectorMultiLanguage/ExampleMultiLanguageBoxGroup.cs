using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Yuumix.OdinToolkits.Examples.InspectorMultiLanguage
{
    public class ExampleMultiLanguageBoxGroup : MonoBehaviour
    {
        [PropertyOrder(-1)]
        public EnumLanguageWidget languageWidget = new EnumLanguageWidget();

        [PropertyOrder(10)]
        [PropertySpace(20)]
        [OnInspectorGUI]
        void Separate1() { }

        [PropertyOrder(30)]
        [PropertySpace(20)]
        [OnInspectorGUI]
        void Separate2() { }

        #region DefaultGroup

        // 没有配置 GroupId 时，使用默认值名称

        /// <summary>
        /// @ 符号前缀，表示这个字符串是一个方法表达式，不需要 ; 结尾
        /// $ 符号前缀，表示接下来的部分字符串是字段名称
        /// Odin 绘制系统中 $property 和 $value 是默认提供的
        /// </summary>
        [PropertyOrder(0)]
        [MultiLanguageBoxGroup]
        [MultiLanguageInfoBox("$GetDefaultGroup")]
        [MultiLanguageInfoBox("@$property.Parent.NiceName")]
        public bool showLabel0 = true;

#if UNITY_EDITOR
        string GetDefaultGroup(InspectorProperty property, string value) =>
            "MultiLanguageBoxGroup 默认组名为 " + property.Parent.NiceName;
#endif

        #endregion

        #region GroupA

        // 可以解析函数方法，需要使用 $ 前缀

        [PropertyOrder(20)]
        [MultiLanguageBoxGroup("GroupA", "$GetGroupNameCn", "$GetGroupNameEn", true)]
        public bool showLabel = true;

        string GetGroupNameCn() => "A 组";

        string GetGroupNameEn() => "A Group";

        #endregion

        #region GroupB

        // 有两个 GroupB，groupId 相同，会合并参数，showLabel: false 优先级更高，所以第一个的 showLabel: true 被覆盖了

        [PropertyOrder(50)]
        [MultiLanguageBoxGroup("GroupB", "B 组", "B", true)]
        public bool showLabel2 = true;

        // 省略了 showLabel: false

        [PropertyOrder(50)]
        [MultiLanguageBoxGroup("GroupB", "C 组", "C")]
        public bool showLabel3 = true;

        #endregion
    }
}
