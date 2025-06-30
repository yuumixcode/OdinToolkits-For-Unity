using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Modules.LearningTests.ForVsForeachPerformance
{
    public class LearningTestForVsForeach : MonoBehaviour
    {
        [MultiLanguageTitle("测试配置", "Options")]
        [SerializeField]
        int testSize =
            1000000; // 测试数据大小

        [SerializeField]
        int iterations =
            10; // 测试迭代次数

        int[] _testArray;
        List<int> _testList;
        LinkedList<int> _testLinkedList;

        [Button("开始性能测试")]
        void StartTest()
        {
            InitializeTestData();
            RunPerformanceTests();
        }

        // 初始化测试数据
        void InitializeTestData()
        {
            _testArray = new int[testSize];
            _testList = new List<int>(testSize);
            _testLinkedList = new LinkedList<int>();

            for (var i = 0; i < testSize; i++)
            {
                _testArray[i] = i;
                _testList.Add(i);
                _testLinkedList.AddLast(i);
            }
        }

        // 运行性能测试
        void RunPerformanceTests()
        {
            Debug.Log($"开始性能测试 - 数据大小: {testSize}, 迭代次数: {iterations}");

            // 测试数组的 for 循环
            var arrayForTime = MeasureTime(() =>
            {
                var sum = 0;
                for (var i = 0; i < _testArray.Length; i++)
                {
                    sum += _testArray[i];
                }
            }, iterations);
            Debug.Log($"数组 For 循环: {arrayForTime:F2} ms");

            // 测试数组的 foreach 循环
            var arrayForeachTime = MeasureTime(() =>
            {
                var sum = 0;
                foreach (var item in _testArray)
                {
                    sum += item;
                }
            }, iterations);
            Debug.Log($"数组 ForEach 循环: {arrayForeachTime:F2} ms");

            // 测试 List 的 for 循环
            var listForTime = MeasureTime(() =>
            {
                var sum = 0;
                for (var i = 0; i < _testList.Count; i++)
                {
                    sum += _testList[i];
                }
            }, iterations);
            Debug.Log($"List For 循环: {listForTime:F2} ms");

            // 测试 List 的 foreach 循环
            var listForeachTime = MeasureTime(() =>
            {
                var sum = 0;
                foreach (var item in _testList)
                {
                    sum += item;
                }
            }, iterations);
            Debug.Log($"List ForEach 循环: {listForeachTime:F2} ms");

            // 测试 LinkedList 的 for 循环 (通过转换为 List)
            var linkedListForTime = MeasureTime(() =>
            {
                var sum = 0;
                var list = new List<int>(_testLinkedList);
                for (var i = 0; i < list.Count; i++)
                {
                    sum += list[i];
                }
            }, iterations);
            Debug.Log($"LinkedList For 循环 (转换为 List): {linkedListForTime:F2} ms");

            // 测试 LinkedList 的 foreach 循环
            var linkedListForeachTime = MeasureTime(() =>
            {
                var sum = 0;
                foreach (var item in _testLinkedList)
                {
                    sum += item;
                }
            }, iterations);
            Debug.Log($"LinkedList ForEach 循环: {linkedListForeachTime:F2} ms");
        }

        // 测量方法执行时间
        double MeasureTime(Action action, int iterations)
        {
            // 预热
            action();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                action();
            }

            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds / iterations;
        }
    }
}
