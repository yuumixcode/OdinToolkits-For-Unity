using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LocalizedDisplayWidgetConfigAttribute : Attribute
    {
        /// <summary>
        /// If <c>true</c>, the string will overflow past the drawn space and be clipped when there's not enough space for the text.
        /// If <c>false</c> the string will expand to multiple lines, if there's not enough space when drawn.
        /// </summary>
        readonly bool _overflow;

        /// <summary>How the string should be aligned.</summary>
        readonly TextAlignment _alignment;

        /// <summary>The size of the font.</summary>
        readonly int _fontSize;

        /// <summary>
        /// If <c>true</c> the string will support rich text.
        /// </summary>
        readonly bool _enableRichText;

        /// <summary>
        /// String for formatting the value. Type must implement the <c>IFormattable</c> interface.
        /// </summary>
        readonly string _format;

        public LocalizedDisplayWidgetConfigAttribute(bool overflow = false,
            TextAlignment alignment = TextAlignment.Left,
            int fontSize = 14, bool enableRichText = false, string format = null)
        {
            _overflow = overflow;
            _alignment = alignment;
            _fontSize = fontSize;
            _enableRichText = enableRichText;
            _format = format ?? string.Empty;
        }

        public DisplayAsStringAttribute CreateDisplayAsStringAttribute()
        {
            return new DisplayAsStringAttribute(_overflow)
            {
                Alignment = _alignment,
                FontSize = _fontSize,
                EnableRichText = _enableRichText,
                Format = _format
            };
        }
    }
}
