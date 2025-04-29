using Plugins.YOGA.Common.Utility.YuumiEditor;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.ComponentBinder.Runtime
{
    /// <summary>
    /// Binder 标签
    /// </summary>
    [DisallowMultipleComponent]
    public class BinderLabel :
#if UNITY_EDITOR
        SerializedMonoBehaviour
#else
        MonoBehaviour
#endif
    {
        [SerializeField]
        [HideInInspector]
        private int componentNumber;

        [ShowInInspector]
        [LabelText("绑定组件数量: ")]
        [PropertyRange(1, "$MaxComponentNumber")]
        public int ComponentNumber
        {
            get => componentNumber;
            set => componentNumber = value;
        }

        private double MaxComponentNumber() => Types.Count();

        public GameObject SelfObj => gameObject;

        public string HierarchyPath
        {
            get
            {
#if UNITY_EDITOR
                return HierarchyUtility.GetAbsolutePath(transform);
#else
                return string.Empty;
#endif
            }
        }

        public IEnumerable<Type> Types
        {
            get
            {
                var types = GetComponents<Component>()
                    .Where(x => x is not BinderLabel)
                    .Select(x => x.GetType()).ToList();
                types.Add(typeof(GameObject));
                return types;
            }
        }
    }
}
