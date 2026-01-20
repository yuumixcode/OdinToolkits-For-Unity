using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BilingualDisplayAsStringWidgetConfigAttribute : Attribute
    {
        public BilingualDisplayAsStringWidgetConfigAttribute(bool overflow = false,
            TextAlignment alignment = TextAlignment.Left, int fontSize = 13, bool enableRichText = false,
            string format = null)
        {
            Overflow = overflow;
            Alignment = alignment;
            FontSize = fontSize;
            EnableRichText = enableRichText;
            Format = format ?? string.Empty;
        }

        public TextAlignment Alignment { get; set; }
        public bool EnableRichText { get; set; }
        public int FontSize { get; set; }

        /// <summary>
        /// String for formatting the value. Type must implement the <c>IFormattable</c> interface.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// If <c>true</c>, the string will overflow past the drawn space and be clipped when there's not enough space
        /// for the
        /// text.
        /// If <c>false</c> the string will expand to multiple lines, if there's not enough space when drawn.
        /// </summary>
        public bool Overflow { get; set; }

        public DisplayAsStringAttribute CreateDisplayAsStringAttribute() =>
            new DisplayAsStringAttribute(Overflow)
            {
                Alignment = Alignment,
                FontSize = FontSize,
                EnableRichText = EnableRichText,
                Format = Format
            };
    }
}
