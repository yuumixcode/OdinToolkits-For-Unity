using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Test
{
    public abstract class TestAbstract
    {
        public abstract void AbstractMethod();
    }

    public class TestScriptDocGenAbstract : TestAbstract
    {
        public override void AbstractMethod()
        {
            Debug.Log("Go");
        }
    }
}
