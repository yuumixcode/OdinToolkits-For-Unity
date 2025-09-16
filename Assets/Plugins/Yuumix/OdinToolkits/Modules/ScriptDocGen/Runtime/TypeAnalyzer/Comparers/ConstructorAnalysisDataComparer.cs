using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules
{
    public class ConstructorAnalysisDataComparer : Comparer<ConstructorAnalysisData>
    {
        public override int Compare(ConstructorAnalysisData x, ConstructorAnalysisData y)
        {
            if (x != null && y != null)
            {
                if (x is MemberAnalysisData xData && y is MemberAnalysisData yData)
                {
                    return new MemberAnalysisDataComparer().Compare(xData, yData);
                }
            }
            else if (x == null && y != null)
            {
                return -1;
            }
            else if (x != null)
            {
                return 1;
            }

            return 0;
        }
    }
}
