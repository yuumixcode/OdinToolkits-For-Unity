using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.Common;

namespace Yuumix.OdinToolkits.Common
{
    /// <summary>
    /// 多语言注释特性，使用特性去提供注释，当使用反射获取成员信息时，可以通过此特性获取注释内容。<br />
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [MultiLanguageComment("多语言注释特性，使用特性去提供注释", "Multi-language comment attribute, provide comment by attribute.")]
    public class MultiLanguageCommentAttribute : Attribute, IMultiLanguageComment
    {
        readonly string _chineseComment;
        readonly string _englishComment;

        /// <summary>
        /// 构造函数，创建具有指定中英文注释的本地化注释特性对象。<br />
        /// Constructor, create LocalizedCommentAttribute instance with specified Chinese and English comments.
        /// </summary>
        /// <param name="chineseComment">中文注释</param>
        /// <param name="englishComment">The English comment</param>
        public MultiLanguageCommentAttribute(string chineseComment, string englishComment = null)
        {
            _chineseComment = chineseComment;
            _englishComment = englishComment ?? "No Comment";
        }

        public string GetChineseComment() => _chineseComment;

        public string GetEnglishComment() => _englishComment;
    }
}
