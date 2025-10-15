using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 继承 MemberData 的数据类的比较类
    /// </summary>
    public class DerivedMemberDataComparer : IComparer<IDerivedMemberData>
    {
        public int Compare(IDerivedMemberData x, IDerivedMemberData y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (y is null)
            {
                return 1;
            }

            if (x is null)
            {
                return -1;
            }

            if (x is MemberData memberDataX && y is MemberData memberDataY)
            {
                var isObsoleteComparison = memberDataX.IsObsolete.CompareTo(memberDataY.IsObsolete);
                if (isObsoleteComparison != 0)
                {
                    return isObsoleteComparison;
                }
            }

            var accessModifierComparison = x.AccessModifier.CompareTo(y.AccessModifier);
            if (accessModifierComparison != 0)
            {
                return accessModifierComparison;
            }

            var isStaticComparison = x.IsStatic.CompareTo(y.IsStatic);
            if (isStaticComparison != 0)
            {
                return isStaticComparison;
            }

            if (x is IFieldData fieldX && y is IFieldData fieldY)
            {
                var constantComparison = fieldX.IsConstant.CompareTo(fieldY.IsConstant);
                if (constantComparison != 0)
                {
                    return constantComparison;
                }

                var readOnlyComparison = fieldX.IsReadOnly.CompareTo(fieldY.IsReadOnly);
                if (readOnlyComparison != 0)
                {
                    return readOnlyComparison;
                }
            }

            return string.Compare(x.Signature, y.Signature, StringComparison.Ordinal);
        }
    }
}
