using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 事件信息数据类
    /// </summary>
    [Serializable]
    public class EventData : MemberData
    {
        #region 公共属性
        
        /// <summary>
        /// 是否为静态事件
        /// </summary>
        public bool IsStatic { get; set; }
        
        /// <summary>
        /// 事件处理器类型
        /// </summary>
        public Type EventHandlerType { get; set; }
        
        /// <summary>
        /// 是否为多播委托
        /// </summary>
        public bool IsMulticast { get; set; }
        
        #endregion
        
        #region 构造函数
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EventData() : base()
        {
        }
        
        /// <summary>
        /// 从 EventInfo 创建 EventData
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        public EventData(EventInfo eventInfo) : base(eventInfo)
        {
        }
        
        #endregion
        
        #region 受保护方法
        
        /// <summary>
        /// 初始化事件特有数据
        /// </summary>
        protected override void InitializeData()
        {
            if (OriginalMemberInfo is EventInfo eventInfo)
            {
                EventHandlerType = eventInfo.EventHandlerType;
                IsMulticast = eventInfo.IsMulticast;
                
                // 检查是否为静态事件
                var addMethod = eventInfo.GetAddMethod(true);
                var removeMethod = eventInfo.GetRemoveMethod(true);
                IsStatic = (addMethod?.IsStatic ?? removeMethod?.IsStatic) == true;
                
                // 设置访问修饰符
                AccessModifier = GetEventAccessModifier(eventInfo);
                
                // 设置类型名称
                TypeName = EventHandlerType?.Name ?? "EventHandler";
                
                // 生成签名
                Signature = GenerateSignature();
                
                // 生成声明
                Declaration = GenerateDeclaration();
            }
        }
        
        /// <summary>
        /// 获取事件访问修饰符
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        /// <returns>访问修饰符类型</returns>
        private AccessModifierType GetEventAccessModifier(EventInfo eventInfo)
        {
            var addMethod = eventInfo.GetAddMethod(true);
            var removeMethod = eventInfo.GetRemoveMethod(true);
            
            AccessModifierType? addAccess = addMethod != null ? GetMethodAccessModifier(addMethod) : null;
            AccessModifierType? removeAccess = removeMethod != null ? GetMethodAccessModifier(removeMethod) : null;
            
            if (!addAccess.HasValue && !removeAccess.HasValue)
                return AccessModifierType.None;
                
            if (!removeAccess.HasValue)
                return addAccess.Value;
                
            if (!addAccess.HasValue)
                return removeAccess.Value;
                
            // 返回较严格的访问级别
            return (int)addAccess.Value <= (int)removeAccess.Value ? addAccess.Value : removeAccess.Value;
        }
        
        /// <summary>
        /// 获取方法访问修饰符
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>访问修饰符类型</returns>
        private AccessModifierType GetMethodAccessModifier(MethodInfo method)
        {
            if (method.IsPublic)
                return AccessModifierType.Public;
            if (method.IsPrivate)
                return AccessModifierType.Private;
            if (method.IsFamily)
                return AccessModifierType.Protected;
            if (method.IsAssembly)
                return AccessModifierType.Internal;
            if (method.IsFamilyOrAssembly)
                return AccessModifierType.ProtectedInternal;
            if (method.IsFamilyAndAssembly)
                return AccessModifierType.PrivateProtected;
                
            return AccessModifierType.None;
        }
        
        /// <summary>
        /// 生成事件签名
        /// </summary>
        /// <returns>事件签名</returns>
        private string GenerateSignature()
        {
            var result = GetAccessModifierString();
            
            if (IsStatic)
                result += " static";
                
            result += $" event {TypeName} {Name}";
            
            return result.Trim();
        }
        
        /// <summary>
        /// 生成事件声明
        /// </summary>
        /// <returns>事件声明</returns>
        private string GenerateDeclaration()
        {
            return GenerateSignature() + ";";
        }
        
        #endregion
        
        #region 公共方法
        
        /// <summary>
        /// 生成格式化的字符串
        /// </summary>
        /// <returns>格式化的字符串</returns>
        public override string ToFormattedString()
        {
            return GenerateSignature();
        }
        
        /// <summary>
        /// 生成详细的描述字符串
        /// </summary>
        /// <returns>详细的描述字符串</returns>
        public override string ToDetailedString()
        {
            var result = ToFormattedString();
            
            // 添加描述
            if (!string.IsNullOrEmpty(ChineseSummary))
                result += $" : {ChineseSummary}";
            else if (!string.IsNullOrEmpty(EnglishSummary))
                result += $" : {EnglishSummary}";
                
            return result;
        }
        
        /// <summary>
        /// 转换为 UML 格式：[可见性][/静态][名称]:[事件类型]
        /// 示例：+ OnClick: EventHandler
        ///      - OnDataChanged: DataChangedEventHandler
        /// </summary>
        /// <returns>UML 格式的事件表示</returns>
        public override string ToUMLString()
        {
            var visibility = GetUMLVisibility(AccessModifier);
            var staticModifier = IsStatic ? "/" : "";
            
            return $"{visibility}{staticModifier}{Name}: {TypeName}";
        }
        
        #endregion
    }
}