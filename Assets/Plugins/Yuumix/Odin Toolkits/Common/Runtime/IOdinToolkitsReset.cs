namespace Yuumix.OdinToolkits.Common
{
    /// <summary>
    /// 由 Editor 程序集中的 ResetTool 控制
    /// </summary>
    public interface IOdinToolkitsReset
    {
        /// <summary>
        /// 插件初始化方法，在插件导出包时，或者需要初始化时，调用此方法
        /// </summary>
        void OdinToolkitsReset();
    }
}
