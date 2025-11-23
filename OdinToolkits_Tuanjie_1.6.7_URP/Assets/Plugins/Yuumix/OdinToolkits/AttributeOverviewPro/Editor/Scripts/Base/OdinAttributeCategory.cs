using System;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [Flags]
    public enum OdinAttributeCategory
    {
        None = 0,
        Essentials = 1,
        Buttons = 2,
        Collections = 4,
        Groups = 8,
        Conditionals = 16,
        Numbers = 32,
        TypeSpecifics = 64,
        Validation = 128,
        Misc = 256,
        Meta = 512,
        Unity = 1024,
        Debug = 2048
    }

    public static class OdinAttributeCategoryExtensions
    {
        public static bool HasFlagFast(this OdinAttributeCategory value, OdinAttributeCategory flag)
        {
            return (value & flag) != 0;
        }
    }
}
