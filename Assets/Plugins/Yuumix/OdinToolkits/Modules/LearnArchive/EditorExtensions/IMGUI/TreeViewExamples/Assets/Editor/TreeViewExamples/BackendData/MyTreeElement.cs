using System;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeDataModel;
using Random = UnityEngine.Random;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeViewExamples.
    BackendData
{
    [Serializable]
    internal class MyTreeElement : TreeElement
    {
        public float floatValue1, floatValue2, floatValue3;
        public Material material;
        public string text = "";
        public bool enabled;

        public MyTreeElement(string name, int depth, int id) : base(name, depth, id)
        {
            floatValue1 = Random.value;
            floatValue2 = Random.value;
            floatValue3 = Random.value;
            enabled = true;
        }
    }
}
