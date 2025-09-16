using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 测试解析构造函数的模版脚本 - A，
    /// 此脚本包含：
    /// 1. 公共实例无参。
    /// 2. 公共实例有参。
    /// 3. 公共实例有参数，包含泛型
    /// </summary>
    public class TestConstructorA
    {
        public int A;
        public List<int> List;
        /// <summary>
        /// 公共实例无参，完美解析
        /// </summary>
        public TestConstructorA() { }

        /// <summary>
        /// 公共实例有参，完美解析
        /// </summary>
        public TestConstructorA(int a)
        {
            A = a;
        }
        /// <summary>
        /// 公共实例有参，参数里面有泛型，完美解析
        /// </summary>
        public TestConstructorA(List<int> list)
        {
            List = list;
        }
    }
}
