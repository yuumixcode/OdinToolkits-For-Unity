using System;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeCategoryAttribute : Attribute
    {
        public AttributeCategoryAttribute(OdinAttributeCategory category) => Category = category;
        public OdinAttributeCategory Category { get; }
    }
}
