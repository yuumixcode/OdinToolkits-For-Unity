using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.ScriptDocGenerator.Editor
{
    public static class ScriptDocGeneratorMenuItems
    {
        const string ADD_SCRIPT_TO_TARGET_TYPE_MENU_NAME =
            "Assets/Script Doc Generator/Add To Target Type";

        const string ADD_AND_OPEN_MENU_NAME =
            "Assets/Script Doc Generator/Add To Target Type And Open Window";

        const string ADD_SCRIPTS_TO_TYPES_MENU_NAME =
            "Assets/Script Doc Generator/Add To Temporary Types";

        const string ADD_SCRIPTS_TO_TYPES_AND_OPEN_MENU_NAME =
            "Assets/Script Doc Generator/Add To Temporary Types And Open Window";

        static MonoScript[] SelectionMonoScripts => Selection
            .GetFiltered(typeof(MonoScript), SelectionMode.Assets).Cast<MonoScript>().ToArray();

        [MenuItem(ADD_SCRIPT_TO_TARGET_TYPE_MENU_NAME, false, 1001)]
        public static void AddScriptToTargetType()
        {
            var monoScript = SelectionMonoScripts.First();
            var targetType = monoScript.GetClass();
            ScriptDocGeneratorPanelSO.Instance.TargetType = targetType;
            ScriptDocGeneratorPanelSO.Instance.TypeSourceProperty =
                ScriptDocGeneratorPanelSO.TypeSource.SingleType;
            Debug.Log("设置 Script Doc Generator 的 Target Type 为：" + targetType.FullName);
        }

        [MenuItem(ADD_AND_OPEN_MENU_NAME, false, 1002)]
        public static void AddScriptToTargetTypeAndOpenWindow()
        {
            AddScriptToTargetType();
            ScriptDocGeneratorWindow.OpenWindow();
        }

        [MenuItem(ADD_SCRIPTS_TO_TYPES_MENU_NAME, false, 1003)]
        public static void AddScriptsToTargetTypes()
        {
            var monoScripts = SelectionMonoScripts.ToList();
            var types = monoScripts.Select(x => x.GetClass()).ToList();
            var temporaryTypes = ScriptDocGeneratorPanelSO.Instance.TemporaryTypes;
            temporaryTypes.AddRange(types);
            var distinctTypes = temporaryTypes.Distinct().ToList();
            ScriptDocGeneratorPanelSO.Instance.TemporaryTypes = distinctTypes;
            ScriptDocGeneratorPanelSO.Instance.TypeSourceProperty =
                ScriptDocGeneratorPanelSO.TypeSource.MultipleTypes;
            foreach (var type in types)
            {
                Debug.Log("添加到 Script Doc Generator 的 Temporary Types：" + type.FullName);
            }
        }

        [MenuItem(ADD_SCRIPTS_TO_TYPES_AND_OPEN_MENU_NAME, false, 1004)]
        public static void AddScriptsToTemporaryTypesAndOpenWindow()
        {
            AddScriptsToTargetTypes();
            ScriptDocGeneratorWindow.OpenWindow();
        }

        [MenuItem(ADD_SCRIPT_TO_TARGET_TYPE_MENU_NAME, true)]
        public static bool AddScriptToTargetTypeValidate()
        {
            var length = SelectionMonoScripts.Length;
            if (length != 1)
            {
                return false;
            }

            var monoScript = SelectionMonoScripts[0];
            var targetType = monoScript.GetClass();
            return targetType != null;
        }

        [MenuItem(ADD_AND_OPEN_MENU_NAME, true)]
        public static bool AddScriptToTargetTypeAndOpenWindowValidate() =>
            AddScriptToTargetTypeValidate();

        [MenuItem(ADD_SCRIPTS_TO_TYPES_MENU_NAME, true)]
        public static bool AddScriptsToTargetTypesValidate()
        {
            var length = SelectionMonoScripts.Length;
            if (length < 1)
            {
                return false;
            }

            foreach (var monoScript in SelectionMonoScripts)
            {
                var targetType = monoScript.GetClass();
                if (targetType == null)
                {
                    return false;
                }
            }

            return true;
        }

        [MenuItem(ADD_SCRIPTS_TO_TYPES_AND_OPEN_MENU_NAME, true)]
        public static bool AddScriptsToTemporaryTypesAndOpenWindowValidate() =>
            AddScriptsToTargetTypesValidate();
    }
}
