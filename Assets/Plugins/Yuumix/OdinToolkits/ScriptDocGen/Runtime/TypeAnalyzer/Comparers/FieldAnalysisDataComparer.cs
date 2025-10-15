using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules
{
    public class FieldAnalysisDataComparer : Comparer<FieldAnalysisData>
    {
        public override int Compare(FieldAnalysisData x, FieldAnalysisData y)
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

                if (x.isConst && !y.isConst)
                {
                    return -1;
                }

                if (!x.isConst && y.isConst)
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

                if (x.isReadonly && !y.isReadonly)
                {
                    return -1;
                }

                if (!x.isReadonly && y.isReadonly)
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
