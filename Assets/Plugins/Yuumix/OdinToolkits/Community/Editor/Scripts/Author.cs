using System;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Community.Editor
{
    [Serializable]
    public class Author
    {
        public string name;
        public string url;

        public override string ToString()
        {
            string au = InspectorMultiLanguageSetting.IsChinese ? "作者: " : "Author: ";
            return au + name.ToYellow() + "  " + url;
        }

        public Author(string name, string url)
        {
            this.name = name;
            this.url = url;
        }
    }
}
