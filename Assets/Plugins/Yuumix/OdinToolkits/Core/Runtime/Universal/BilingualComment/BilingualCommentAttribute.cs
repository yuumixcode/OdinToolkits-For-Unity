using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    /// <summary>
    /// 中文和英文双语注释特性，使用特性去提供注释，当使用反射获取成员信息时，可以通过此特性获取注释内容。<br />
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [BilingualComment("中文和英文双语注释特性，使用特性去提供注释", "Multi-language comment attribute, provide comment by attribute.")]
    public class BilingualCommentAttribute : Attribute, IBilingualComment
    {
        readonly string _chinese;
        readonly string _english;

        /// <summary>
        /// 构造函数，创建具有指定中英文注释的本地化注释特性对象。<br />
        /// Constructor, create LocalizedCommentAttribute instance with specified Chinese and English comments.
        /// </summary>
        /// <param name="chinese">中文注释</param>
        /// <param name="englishComment">The English comment</param>
        public BilingualCommentAttribute(string chinese, string englishComment = null)
        {
            _chinese = chinese;
            _english = englishComment ?? "No Comment";
        }

        public string GetChinese() => _chinese;

        public string GetEnglish() => _english;
    }
}
