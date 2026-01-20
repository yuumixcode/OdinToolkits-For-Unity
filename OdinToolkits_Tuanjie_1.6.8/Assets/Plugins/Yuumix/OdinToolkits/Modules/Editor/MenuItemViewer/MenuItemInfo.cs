using System;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Module.Editor
{
    /// <summary>
    /// 存储 UnityEditor.MenuItem 特性的参数信息
    /// </summary>
    [Summary("存储 UnityEditor.MenuItem 特性的参数信息")]
    [Serializable]
    public class MenuItemInfo : ISearchFilterable
    {
        public MenuItemInfo(MenuItem menuItem, MethodInfo method)
        {
            MenuPath = menuItem.menuItem;
            IsValidateFunction = menuItem.validate;
            Priority = menuItem.priority;
            Method = method;
            Assembly = method.DeclaringType?.Assembly;
            ClassName = method.DeclaringType?.Name;
            MethodName = method.Name;
            FullMethodSignature = $"{ClassName}.{MethodName}()";
        }

        /// <summary>
        /// 菜单路径
        /// </summary>
        [Summary("菜单路径")]
        [ShowEnableProperty]
        [DisplayAsString]
        public string MenuPath { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Summary("优先级")]
        [ShowEnableProperty]
        [DisplayAsString]
        public int Priority { get; }

        /// <summary>
        /// 是否是验证方法
        /// </summary>
        [Summary("是否是验证方法")]
        public bool IsValidateFunction { get; set; }

        /// <summary>
        /// 所属方法
        /// </summary>
        [Summary("所属方法")]
        public MethodInfo Method { get; set; }

        /// <summary>
        /// 所属程序集
        /// </summary>
        [Summary("所属程序集")]
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 所属类名
        /// </summary>
        [Summary("所属类名")]
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        [Summary("方法名")]
        public string MethodName { get; set; }

        /// <summary>
        /// 完整的方法签名
        /// </summary>
        [Summary("完整的方法签名")]
        public string FullMethodSignature { get; set; }

        #region ISearchFilterable Members

        /// <summary>
        /// ISearchFilterable 接口方法，自定义搜索匹配规则
        /// </summary>
        [Summary("ISearchFilterable 接口方法，自定义搜索匹配规则")]
        public bool IsMatch(string searchString) =>
            MenuPath.ToLower()
                .Contains(searchString.ToLower()) || MethodName.ToLower()
                .Contains(searchString.ToLower());

        #endregion
    }
}
