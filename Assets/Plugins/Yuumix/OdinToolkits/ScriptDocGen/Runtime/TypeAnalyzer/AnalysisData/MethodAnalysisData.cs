using System;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class MethodAnalysisData : MemberAnalysisData
    {
        #region Serialized Fields

        public string parametersString;
        public bool isStaticMethod;
        public bool isAbstract;
        public bool isVirtual;
        public bool isOperator;
        public bool isOverride;

        #endregion

        #region 特殊状态补充

        /// <summary>
        /// 此方法继承自祖先（不是直接的基类，而是基类的上层）
        /// </summary>
        public bool isFromAncestor;

        /// <summary>
        /// 此方法继承自接口，在该类中实现
        /// </summary>
        public bool isFromInterfaceImplement;

        /// <summary>
        /// 此方法在当前类中存在重载方法
        /// </summary>
        public bool isOverloadMethodInDeclaringType;

        #endregion
    }
}
