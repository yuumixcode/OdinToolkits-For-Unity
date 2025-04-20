using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YOGA.Modules.Utilities;

namespace YOGA.Modules.Object_Binder.Common
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
        int componentNumber;

        [ShowInInspector]
        [LabelText("绑定组件数量: ")]
        [PropertyRange(1, "$MaxComponentNumber")]
        public int ComponentNumber
        {
            get => componentNumber;
            set => componentNumber = value;
        }

        double MaxComponentNumber()
        {
            return Types.Count();
        }

        public GameObject SelfObj
        {
            get => gameObject;
        }

        public string HierarchyPath
        {
            get => HierarchyUtility.GetAbsolutePath(transform);
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
