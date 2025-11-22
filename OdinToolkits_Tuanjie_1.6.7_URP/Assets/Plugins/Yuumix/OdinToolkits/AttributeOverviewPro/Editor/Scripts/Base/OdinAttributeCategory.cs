using System;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [Flags]
    public enum OdinAttributeCategory
    {
        None = 0,
        Essential = 1,
        Button = 2,
        Collection = 4,
        Group = 8,
        Conditional = 16,
        Number = 32,
        TypeSpecific = 64,
        Validation = 128,
        Misc = 256,
        Meta = 512,
        Unity = 1024,
        Debug = 2048
    }
}
