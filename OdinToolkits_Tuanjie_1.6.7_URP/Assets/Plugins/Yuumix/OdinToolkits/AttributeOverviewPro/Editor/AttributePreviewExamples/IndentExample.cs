using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class IndentExample : ExampleSO
    {
        #region Serialized Fields

        [Title("Nicely organize your properties.")]
        [Indent]
        public int A;

        [Indent(2)]
        public int B;

        [Indent(3)]
        public int C;

        [Indent(4)]
        public int D;

        [Title("Using the Indent attribute")]
        [Indent]
        public int E;

        [Indent(0)]
        public int F;

        [Indent(-1)]
        public int G;

        #endregion
    }
}
