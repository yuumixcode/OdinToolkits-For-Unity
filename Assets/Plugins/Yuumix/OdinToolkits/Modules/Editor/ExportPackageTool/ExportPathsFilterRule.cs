using Yuumix.OdinToolkits.Core;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 导出文件路径过滤规则抽象类
    /// </summary>
    [Summary("导出文件路径过滤规则抽象类")]
    public abstract class ExportPathsFilterRule
    {
        /// <summary>
        /// 判断文件路径是否被排除
        /// </summary>
        [Summary("判断文件路径是否被排除")]
        public abstract bool IsExcludedFile(string filePath);
        /// <summary>
        /// 判断文件夹路径是否被排除
        /// </summary>
        [Summary("判断文件夹路径是否被排除")]
        public abstract bool IsExcludedFolder(string folderPath);
    }
}
