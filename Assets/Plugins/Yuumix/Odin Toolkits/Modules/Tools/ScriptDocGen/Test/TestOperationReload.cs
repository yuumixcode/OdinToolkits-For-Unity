using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Test
{
    public class TestOperationReload
    {
        int _num;

        public static TestOperationReload operator +(TestOperationReload x, TestOperationReload y)
        {
            return new TestOperationReload() { _num = x._num + y._num };
        }
    }
}
