using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Community.Editor
{
    [Serializable]
    public class Author
    {
        string _name;
        string _url;

        public Author(string name, string url = "")
        {
            _name = name;
            _url = url;
        }

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
                var prefix = InspectorBilingualismConfigSO.IsChinese ? "作者: " : "Author: ";
                return prefix + _name.ToYellow();
            }
        }

        [PropertyOrder(3)]
        [HorizontalGroup]
        [Button("$_url", 16)]
        public void ButtonGUI()
        {
            Application.OpenURL(_url);
        }
    }
}
