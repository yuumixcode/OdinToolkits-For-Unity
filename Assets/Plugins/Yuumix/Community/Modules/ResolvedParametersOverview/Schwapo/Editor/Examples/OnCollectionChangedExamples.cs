using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class OnCollectionChangedExamples_Before
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Edit the collection to see this attribute in effect")]
        [OnCollectionChanged(Before = "@Debug.Log(\"A change is going to occur.\")")]
        public List<string> AttributeExpressionExample = new List<string>();

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Edit the collection to see this attribute in effect")]
        [OnCollectionChanged(Before = "BeforeChange")]
        public List<string> MethodNameExample = new List<string>();

        void BeforeChange(CollectionChangeInfo info)
        {
            Debug.Log("Change of type: " + info.ChangeType + " is going to occur");
        }
    }
    // End

    [ResolvedParameterExample]
    public class OnCollectionChangedExamples_After
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Edit the collection to see this attribute in effect")]
        [OnCollectionChanged(After = "@Debug.Log(\"A change occurred.\")")]
        public List<string> AttributeExpressionExample = new List<string>();

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Edit the collection to see this attribute in effect")]
        [OnCollectionChanged(After = "AfterChange")]
        public List<string> MethodNameExample = new List<string>();

        void AfterChange(CollectionChangeInfo info)
        {
            Debug.Log("Change of type: " + info.ChangeType + " occured");
        }
    }
    // End
}
