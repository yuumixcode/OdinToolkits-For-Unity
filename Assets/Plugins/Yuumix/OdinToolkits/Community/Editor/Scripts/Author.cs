using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Runtime;

namespace Yuumix.OdinToolkits.Community.Editor
{
    [Serializable]
    public class Author
    {
        string _name;
        string _url;

        [PropertyOrder(1)]
        [ShowInInspector]
        [HideLabel]
        [EnableGUI]
        [DisplayAsString(EnableRichText = true, FontSize = 14)]
        [HorizontalGroup]
        public string Name
        {
            get
            {
                string prefix = BilingualSetting.IsChinese ? "作者: " : "Author: ";
                return prefix + _name.ToYellow();
            }
        }

        [PropertyOrder(3)]
        [HorizontalGroup]
        [Button("$_url", buttonSize: 16)]
        public void ButtonGUI()
        {
            Application.OpenURL(_url);
        }

        public Author(string name, string url = "")
        {
            _name = name;
            _url = url;
        }
    }
}
