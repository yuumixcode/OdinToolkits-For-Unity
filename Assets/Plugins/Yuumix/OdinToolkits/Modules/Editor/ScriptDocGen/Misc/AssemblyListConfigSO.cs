using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class AssemblyListConfigSO : SerializedScriptableObject
    {
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        public Dictionary<string, List<Type>> AssemblyStringTypesMap = new Dictionary<string, List<Type>>();
    }
}
