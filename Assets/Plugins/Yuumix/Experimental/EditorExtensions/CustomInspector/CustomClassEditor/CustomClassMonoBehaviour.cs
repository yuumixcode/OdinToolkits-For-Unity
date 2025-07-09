using System;
using UnityEngine;

namespace Odin_Toolkits.Inspector.CustomInspectorTutorial._3.自定义数据结构类
{
    public class CustomClassMonoBehaviour : MonoBehaviour
    {
        [Serializable]
        public class TempClass
        {
            public int intValue;
            public string stringValue;
        }

        public TempClass customData = new TempClass();

        // Start is called before the first frame update
        void Start() { }

        // Update is called once per frame
        void Update() { }
    }
}
