using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Modules.CustomAttributes.Editor
{
    public class RequiredInterfaceAttributeDrawer<TObject> :
        OdinAttributeDrawer<RequiredInterfaceAttribute, TObject>
        where TObject : Object
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Type interfaceType = Attribute.InterfaceType;
            TObject referenceValue = Property.TryGetTypedValueEntry<TObject>().SmartValue;
            if (referenceValue == null)
            {
                SirenixEditorGUI.ErrorMessageBox($"没有实现 {interfaceType.Name} 接口的实例对象或 ScriptableObject 资产");
            }

            SirenixEditorGUI.BeginHorizontalPropertyLayout(new GUIContent($"{label} [{interfaceType.Name}] "));
            {
                referenceValue = SirenixEditorFields.UnityObjectField(
                    GUIContent.none,
                    referenceValue,
                    typeof(TObject),
                    true) as TObject;
                const float squareSize = 14f;
                GUILayout.Box(new GUIContent(), SirenixGUIStyles.None,
                    GUILayoutOptions.Height(22F).MinWidth(squareSize + 2).MaxWidth(22F));
                Rect lastRect = GUILayoutUtility.GetLastRect();
                Rect innerRect = lastRect.AlignCenterXY(squareSize, squareSize);
                // SirenixEditorGUI.DrawBorders(lastRect, 1, Color.cyan);
                ValidateAndDrawIcon(ref referenceValue, interfaceType, innerRect, squareSize);
            }
            SirenixEditorGUI.EndHorizontalPropertyLayout();
            Property.TryGetTypedValueEntry<TObject>().SmartValue = referenceValue;
        }

        static void ValidateAndDrawIcon(ref TObject target, Type interfaceType, Rect innerRect,
            float squareSize)
        {
            var isValid = false;
            if (target)
            {
                switch (target)
                {
                    case MonoBehaviour monoBehaviour:
                        target = monoBehaviour.gameObject.GetComponent(interfaceType) as TObject;
                        break;
                    case GameObject gameObject:
                        target = gameObject.GetComponent(interfaceType) as TObject;
                        break;
                    default:
                    {
                        Type targetType = target.GetType();
                        target = interfaceType.IsAssignableFrom(targetType) ? target : null;
                        break;
                    }
                }

                isValid = target;
            }

            SirenixEditorGUI.DrawRoundRect(innerRect,
                isValid ? SirenixGUIStyles.GreenValidColor : SirenixGUIStyles.RedErrorColor, squareSize / 2);
        }
    }
}
