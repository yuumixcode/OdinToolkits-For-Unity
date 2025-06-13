using System;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor
{
    public class MemberRawDataComparer : Comparer<MemberRawData>
    {
        public override int Compare(MemberRawData x, MemberRawData y)
        {
            switch (x)
            {
                case null when y == null:
                    return 0;
                case null:
                    return -1;
            }

            if (y == null)
            {
                return 1;
            }

            if ((x.declaringTypeName == x.BelongToType.Name) != (y.declaringTypeName == y.BelongToType.Name))
            {
                return x.declaringTypeName == x.BelongToType.Name ? -1 : 1;
            }

            if (ReflectionUtil.CustomMemberTypesMap[x.memberType] != ReflectionUtil.CustomMemberTypesMap[y.memberType])
            {
                return ReflectionUtil.CustomMemberTypesMap[x.memberType]
                    .CompareTo(ReflectionUtil.CustomMemberTypesMap[y.memberType]);
            }

            if (x.modifierType != y.modifierType)
            {
                return x.modifierType.CompareTo(y.modifierType);
            }

            if (x.isStatic != y.isStatic)
            {
                return x.isStatic ? -1 : 1;
            }

            if (x.isVirtual != y.isVirtual)
            {
                return x.isVirtual ? -1 : 1;
            }

            if (x.isAbstract != y.isAbstract)
            {
                return x.isAbstract ? -1 : 1;
            }

            if (x.rawName.Contains(".") && !y.rawName.Contains("."))
            {
                return 1;
            }

            if (!x.rawName.Contains(".") && y.rawName.Contains("."))
            {
                return -1;
            }

            return string.Compare(x.rawName, y.rawName, StringComparison.Ordinal);
        }
    }
}
