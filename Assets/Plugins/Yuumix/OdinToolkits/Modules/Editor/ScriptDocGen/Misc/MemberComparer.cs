using System;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class MemberComparer : Comparer<MemberData>
    {
        public override int Compare(MemberData x, MemberData y)
        {
            if (x != null && y != null)
            {
                if ((int)x.memberAccessModifierType < (int)y.memberAccessModifierType)
                {
                    return -1;
                }

                if ((int)x.memberAccessModifierType != (int)y.memberAccessModifierType)
                {
                    return 1;
                }

                if (x.isStatic && !y.isStatic)
                {
                    return -1;
                }

                if (!x.isStatic && y.isStatic)
                {
                    return 1;
                }

                if (x.NoFromInherit && !y.NoFromInherit)
                {
                    return -1;
                }

                if (!x.NoFromInherit && y.NoFromInherit)
                {
                    return 1;
                }

                return string.Compare(x.name, y.name, StringComparison.Ordinal);
            }

            if (x == null && y != null)
            {
                return -1;
            }

            return 1;
        }
    }
}
