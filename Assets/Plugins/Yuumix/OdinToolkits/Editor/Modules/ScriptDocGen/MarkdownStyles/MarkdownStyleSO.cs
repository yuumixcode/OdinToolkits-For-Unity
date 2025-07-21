using System.Text;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    public abstract class MarkdownStyleSO : ScriptableObject
    {
        public abstract string GetMarkdownText(TypeData data, DocCategory docCategory, StringBuilder identifier);
    }
}
