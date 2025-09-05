using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public static class ColorExtensions
    {
        /// <summary>
        /// 将 Color 对象转换为 HEX 十六进制颜色码（例如："FF00FF"）
        /// </summary>
        /// <param name="color">要转换的颜色</param>
        /// <param name="includeAlpha">是否包含 Alpha 通道</param>
        /// <returns>十六进制颜色字符串</returns>
        public static string ToHexString(this Color color, bool includeAlpha = false)
        {
            int r = Mathf.RoundToInt(color.r * 255);
            int g = Mathf.RoundToInt(color.g * 255);
            int b = Mathf.RoundToInt(color.b * 255);
            int a = Mathf.RoundToInt(color.a * 255);

            return includeAlpha ? $"{r:X2}{g:X2}{b:X2}{a:X2}" : $"{r:X2}{g:X2}{b:X2}";
        }
    }
}
