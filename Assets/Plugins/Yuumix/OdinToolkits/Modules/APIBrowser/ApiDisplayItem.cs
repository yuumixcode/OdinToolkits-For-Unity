using System;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Yuumix.OdinToolkits.Modules.APIBrowser
{
    [Serializable]
    public class ApiDisplayItem
    {
        [HideInInspector]
        public ApiRawData rawData;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string memberType;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string MemberName => rawData.rawName;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string FullMemberSignature => rawData.fullSignature;

        [ShowInInspector]
        [DisplayAsString]
        [EnableGUI]
        public string DeclaringTypeName => rawData.declaringTypeName;
    }
}
