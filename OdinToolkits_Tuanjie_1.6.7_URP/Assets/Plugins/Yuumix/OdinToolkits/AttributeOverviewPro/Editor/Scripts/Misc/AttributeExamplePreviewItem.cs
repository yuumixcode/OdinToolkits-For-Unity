using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public enum AttributeExampleType
    {
        UnitySerialized,
        OdinSerialized
    }

    public class AttributeExamplePreviewItem
    {
        SerializedScriptableObject _odinSerializedExample;
        ScriptableObject _unitySerializedExample;
        public AttributeExampleType ExampleType { get; private set; }
        public string ItemName { get; private set; }

        public ScriptableObject UnitySerializedExample
        {
            get
            {
                if (ExampleType == AttributeExampleType.UnitySerialized)
                {
                    return _unitySerializedExample;
                }

                Debug.LogError("Odin 序列化的案例应该获取 " + nameof(OdinSerializedExample));
                return null;
            }
        }

        public SerializedScriptableObject OdinSerializedExample
        {
            get
            {
                if (ExampleType == AttributeExampleType.OdinSerialized)
                {
                    return _odinSerializedExample;
                }

                Debug.LogError("Unity 原生序列化的案例应该获取 " + nameof(UnitySerializedExample));
                return null;
            }
        }

        public AttributeExamplePreviewItem InitializeOdinSerializedExample(string itemName,
            SerializedScriptableObject serializedExample)
        {
            ExampleType = AttributeExampleType.OdinSerialized;
            ItemName = itemName;
            _odinSerializedExample = serializedExample;
            return this;
        }

        public AttributeExamplePreviewItem InitializeUnitySerializedExample(string itemName,
            ScriptableObject unitySerializedExample)
        {
            ExampleType = AttributeExampleType.UnitySerialized;
            ItemName = itemName;
            _unitySerializedExample = unitySerializedExample;
            return this;
        }

        public void Reset()
        {
            switch (ExampleType)
            {
                case AttributeExampleType.OdinSerialized:
                    if (_odinSerializedExample is IOdinToolkitsEditorReset canResetOdinSerializedExample)
                    {
                        AttributeOverviewProUtility.LogEditorResetSuccess(_odinSerializedExample.GetType().Name);
                        canResetOdinSerializedExample.EditorReset();
                    }
                    else
                    {
                        AttributeOverviewProUtility.LogEditorResetWarning(_odinSerializedExample.GetType().Name);
                    }

                    break;
                case AttributeExampleType.UnitySerialized:
                    if (_unitySerializedExample is IOdinToolkitsEditorReset canResetUnitySerializedExample)
                    {
                        AttributeOverviewProUtility.LogEditorResetSuccess(_unitySerializedExample.GetType().Name);
                        canResetUnitySerializedExample.EditorReset();
                    }
                    else
                    {
                        AttributeOverviewProUtility.LogEditorResetWarning(_unitySerializedExample.GetType().Name);
                    }

                    break;
            }
        }
    }
}
