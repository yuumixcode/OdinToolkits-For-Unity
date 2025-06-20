using Sirenix.OdinInspector.Editor;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public interface ICanSetBelongToWindow
    {
        void SetWindow(OdinMenuEditorWindow window);
        void ClearWindow();
    }
}
