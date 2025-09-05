﻿using System.Collections.Generic;
using System.Linq;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [BilingualComment("集合工具类", "Collection utility class")]
    public static class CollectionUtility
    {
        [BilingualComment("合并两个字符串集合并去重", "Merge two string collections and remove duplicates")]
        public static string[] MergeAndRemoveDuplicates(IEnumerable<string> array1, IEnumerable<string> array2)
        {
            // 使用HashSet<string>来快速合并和去重
            var uniqueStrings = new HashSet<string>(array1);

            // 添加第二个数组中的元素到HashSet中，重复的会被自动忽略
            foreach (string item in array2)
            {
                uniqueStrings.Add(item);
            }

            // 将去重后的集合转换为数组并排序
            return uniqueStrings.OrderBy(s => s).ToArray();
        }
    }
}
