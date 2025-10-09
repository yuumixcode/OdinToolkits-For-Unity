using System;
using System.Reflection;
using System.Text;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class PropertyAnalysisData : MemberAnalysisData
    {
        public bool isStatic;
        public AccessModifierType getAccessModifierType;
        public AccessModifierType setAccessModifierType;
        public string GetAccessModifier => getAccessModifierType.ConvertToString();
        public string SetAccessModifier => setAccessModifierType.ConvertToString();
    }
}
