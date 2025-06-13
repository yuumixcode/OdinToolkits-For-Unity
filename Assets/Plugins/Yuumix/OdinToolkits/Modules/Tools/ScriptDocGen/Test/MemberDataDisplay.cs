using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Modules.Tools.GenerateDocumentation.Runtime;
using Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.GenerateDocumentation.Test
{
    public class MemberDataDisplay : SerializedMonoBehaviour
    {
        [PropertyOrder] [ShowInInspector] public static Type TestType => typeof(OdinMenuEditorWindow);

        public MemberInfo Field;

        public List<MemberData> memberDataList;

        public List<string> list;
        public TypeData typeData;

        public Type Type;

        [Button]
        public void LogMemberInfo()
        {
            memberDataList = ReflectionUtility.GetTypeMemberDataWithNonPublic(typeof(MemberDataForTest));
            list = memberDataList.Select(x => x.ToString()).ToList();
        }

        [Button]
        public void CreateTypeData()
        {
            typeData = TypeData.FromType(TestType);
        }

        [Serializable]
        class Custom : UnityEngine.Object
        {
            public int a;
        }
    }
}
