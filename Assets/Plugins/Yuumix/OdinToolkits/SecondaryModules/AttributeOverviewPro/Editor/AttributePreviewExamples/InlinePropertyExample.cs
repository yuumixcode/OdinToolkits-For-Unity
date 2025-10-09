using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class InlinePropertyExample : ExampleSO
    {
        [Title("Unity 原生绘制样式 Vector3")]
        public Vector3 vector3;

        [Title("自定义结构体 [InlineProperty] 作用于特定字段")]
        [InfoBox("自定义类不使用 [InlineProperty] 时")]
        public Vector2Int myVector2Int;

        [InfoBox("使用 [InlineProperty] ")]
        [InlineProperty]
        public Vector2Int myVector2Int2;

        [Title("自定义结构体 [InlineProperty] 作用于类")]
        public Vector3Int myVector3Int;

        [Serializable]
        [InlineProperty(LabelWidth = 13)]
        [TypeInfoBox("通常设置为 13，Unity 此类型样式使用 13 像素宽度")]
        public struct Vector3Int
        {
            [HorizontalGroup]
            public int X;

            [HorizontalGroup]
            public int Y;

            [HorizontalGroup]
            public int Z;
        }

        [Serializable]
        public struct Vector2Int
        {
            [HorizontalGroup]
            public int X;

            [HorizontalGroup]
            public int Y;
        }
    }
}
