using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class MultiLanguageDisplayWidgetConfigAttribute : Attribute
    {
        /// <summary>
        /// If <c>true</c>, the string will overflow past the drawn space and be clipped when there's not enough space for the
        /// text.
        /// If <c>false</c> the string will expand to multiple lines, if there's not enough space when drawn.
        /// </summary>
        readonly bool _overflow;

        readonly TextAlignment _alignment;

        readonly int _fontSize;

        readonly bool _enableRichText;

        /// <summary>
        /// String for formatting the value. Type must implement the <c>IFormattable</c> interface.
        /// </summary>
        readonly string _format;

        public MultiLanguageDisplayWidgetConfigAttribute(bool overflow = false,
            TextAlignment alignment = TextAlignment.Left,
            int fontSize = 14, bool enableRichText = false, string format = null)
        {
            _overflow = overflow;
            _alignment = alignment;
            _fontSize = fontSize;
            _enableRichText = enableRichText;
            _format = format ?? string.Empty;
        }

        public DisplayAsStringAttribute CreateDisplayAsStringAttribute() =>
            new DisplayAsStringAttribute(_overflow)
            {
                Alignment = _alignment,
                FontSize = _fontSize,
                EnableRichText = _enableRichText,
                Format = _format
            };
    }
}
