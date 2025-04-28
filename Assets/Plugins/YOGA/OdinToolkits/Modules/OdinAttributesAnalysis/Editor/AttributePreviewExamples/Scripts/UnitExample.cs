using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class UnitExample : ExampleScriptableObject
    {
        // Try entering '6 lb'.
        [FoldoutGroup("一个参数")] [Unit(Units.Kilogram)]
        public float weight;

        // Try entering '15 mph'.
        // 面板上输入的值是以 Units.KilometersPerHour 为单位，
        // speed 变量的实际值会换算成以 Units.MetersPerSecond 为单位的值
        [FoldoutGroup("两个参数，一个实际单位，一个显示单位")]
        [Unit(Units.MetersPerSecond, Units.KilometersPerHour)]
        [InlineButton("Log1", "输出实际值")]
        public float speed;

        [FoldoutGroup("两个参数，一个实际单位，一个显示单位")] [Unit(Units.Centimeter, Units.Kilometer)] [InlineButton("Log2", "输出实际值")]
        public float distance;

        // UxmlAttributeDescription.Use the custom unit by referencing it by name.
        [FoldoutGroup("自定义单位，实际单位为米")] [Unit(Units.Meter, "Odin Toolkits Custom Unit")] [InlineButton("Log3", "输出实际值")]
        public float odin;

        [FoldoutGroup("两个参数，一个实际单位，一个显示单位")]
        [ShowInInspector]
        [Unit(Units.MetersPerSecond, Units.MilesPerHour,
            DisplayAsString = true, ForceDisplayUnit = true)]
        public float SpeedMilesPerHour => speed;

        private void Log1()
        {
            Debug.Log(nameof(speed) + ": " + speed + "米每秒");
        }

        private void Log2()
        {
            Debug.Log(nameof(distance) + ": " + distance + "厘米");
        }

        private void Log3()
        {
            Debug.Log(nameof(odin) + ": " + odin + "米");
        }

        // Add custom units. (Disabled to not add custom units to your project)
        [InitializeOnLoadMethod]
        public static void AddCustomUnit()
        {
            UnitNumberUtility.AddCustomUnit(
                "Odin Toolkits Custom Unit",
                new[] { "Odin Toolkits Custom Unit" },
                UnitCategory.Distance,
                1m / 77m);
        }
    }
}