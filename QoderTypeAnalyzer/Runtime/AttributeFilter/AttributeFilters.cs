using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 特性过滤器静态便捷方法
    /// </summary>
    public static class AttributeFilters
    {
        #region 公共方法
        
        /// <summary>
        /// 创建仅包含文档相关特性的过滤器
        /// </summary>
        /// <returns>文档过滤器</returns>
        public static IAttributeFilter CreateDocumentationFilter()
        {
            var filter = new DefaultAttributeFilter();
            
            // 排除所有编译器和调试相关特性
            filter.ExcludeAttributeByName("CompilerGenerated");
            filter.ExcludeAttributeByName("DebuggerBrowsable");
            filter.ExcludeAttributeByName("DebuggerHidden");
            filter.ExcludeAttributeByName("DebuggerDisplay");
            filter.ExcludeAttributeByName("DebuggerStepThrough");
            
            // 排除序列化特性
            filter.ExcludeAttribute<SerializableAttribute>();
            filter.ExcludeAttributeByName("NonSerialized");
            
            // 排除项目特定特性
            filter.ExcludeAttribute<ChineseSummaryAttribute>();
            
            return filter;
        }
        
        /// <summary>
        /// 创建仅显示公共 API 特性的过滤器
        /// </summary>
        /// <returns>公共 API 过滤器</returns>
        public static IAttributeFilter CreatePublicApiFilter()
        {
            var filter = CreateDocumentationFilter();
            
            // 排除更多内部实现相关特性
            filter.ExcludeAttributeByName("EditorBrowsable");
            filter.ExcludeAttributeByName("Browsable");
            filter.ExcludeAttributeByName("DefaultValue");
            
            return filter;
        }
        
        /// <summary>
        /// 创建包含所有特性的过滤器（不过滤任何特性）
        /// </summary>
        /// <returns>全包含过滤器</returns>
        public static IAttributeFilter CreateIncludeAllFilter()
        {
            var filter = new DefaultAttributeFilter();
            filter.ClearExclusions();
            return filter;
        }
        
        /// <summary>
        /// 创建最小化特性过滤器（仅保留最基本的特性）
        /// </summary>
        /// <returns>最小化过滤器</returns>
        public static IAttributeFilter CreateMinimalFilter()
        {
            var filter = CreateDocumentationFilter();
            
            // 排除更多特性，仅保留最基本的
            filter.ExcludeAttribute<ObsoleteAttribute>();
            filter.ExcludeAttributeByName("Description");
            filter.ExcludeAttributeByName("DisplayName");
            filter.ExcludeAttributeByName("Category");
            
            return filter;
        }
        
        #endregion
    }
}