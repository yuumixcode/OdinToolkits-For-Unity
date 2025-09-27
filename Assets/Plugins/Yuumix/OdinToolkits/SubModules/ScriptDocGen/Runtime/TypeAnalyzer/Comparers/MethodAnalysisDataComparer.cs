using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules
{
    public class MethodAnalysisDataComparer : Comparer<MethodAnalysisData>
    {
        public override int Compare(MethodAnalysisData x, MethodAnalysisData y)
        {
            if (x != null && y != null)
            {
                if ((int)x.memberAccessModifierType < (int)y.memberAccessModifierType)
                {
                    return -1;
                }

                if ((int)x.memberAccessModifierType > (int)y.memberAccessModifierType)
                {
                    return 1;
                }

                if (x.isStaticMethod && !y.isStaticMethod)
                {
                    return -1;
                }

                if (!x.isStaticMethod && y.isStaticMethod)
                {
                    return 1;
                }

                if (!x.IsFromInheritMember() && y.IsFromInheritMember())
                {
                    return -1;
                }

                if (x.IsFromInheritMember() && !y.IsFromInheritMember())
                {
                    return 1;
                }

                return string.Compare(x.name, y.name, StringComparison.Ordinal);
            }

            if (x == null && y != null)
            {
                return -1;
            }

            return x != null ? 1 : 0;
        }
    }
}
