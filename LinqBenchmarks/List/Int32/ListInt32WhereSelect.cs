﻿using BenchmarkDotNet.Attributes;
using JM.LinqFaster;
using NetFabric.Hyperlinq;
using StructLinq;
using System.Linq;

namespace LinqBenchmarks.List.Int32
{
    public class ListInt32WhereSelect: Int32ListBenchmarkBase
    {
        [Benchmark(Baseline = true)]
        public int ForLoop()
        {
            var sum = 0;
            for (var index = 0; index < source.Count; index++)
            {
                var item = source[index];
                if (item.IsEven())
                    sum += item * 3;
            }
            return sum;
        }

#pragma warning disable HLQ010 // Consider using a 'for' loop instead.
        [Benchmark]
        public int ForeachLoop()
        {
            var sum = 0;
            foreach (var item in source)
            {
                if (item.IsEven())
                    sum += item * 3;
            }
            return sum;
        }
#pragma warning restore HLQ010 // Consider using a 'for' loop instead.

        [Benchmark]
        public int Linq()
        {
            var sum = 0;
            foreach (var item in System.Linq.Enumerable.Where(source, item => item.IsEven()).Select(item => item * 3))
                sum += item;
            return sum;
        }

        [Benchmark]
        public int LinqFaster()
        {
            var items = source.WhereSelectF(item => item.IsEven(), item => item * 3);
            var sum = 0;
            for (var index = 0; index < items.Count; index++)
                sum += items[index];
            return sum;
        }

        [Benchmark]
        public int LinqAF()
        {
            var sum = 0;
            foreach (var item in global::LinqAF.ListExtensionMethods.Where(source, item => item.IsEven()).Select(item => item * 3))
                sum += item;
            return sum;
        }

        [Benchmark]
        public int StructLinq()
        {
            var sum = 0;
            foreach (var item in source
                .ToStructEnumerable()
                .Where(item => item.IsEven())
                .Select(item => item * 3))
                sum += item;
            return sum;
        }

        [Benchmark]
        public int StructLinq_IFunction()
        {
            var sum = 0;
            var predicate = new Int32IsEven();
            var selector = new TripleOfInt32();
            foreach (var item in source
                .ToStructEnumerable()
                .Where(ref predicate, x => x)
                .Select(ref selector, x => x, x => x))
                sum += item;
            return sum;
        }

        [Benchmark]
        public int Hyperlinq()
        {
            var sum = 0;
            foreach (var item in source.AsValueEnumerable()
                .Where(item => item.IsEven())
                .Select(item => item * 3))
                sum += item;
            return sum;
        }

        [Benchmark]
        public int Hyperlinq_IFunction()
        {
            var sum = 0;
            foreach (var item in source.AsValueEnumerable()
                .Where<Int32IsEven>()
                .Select<int, TripleOfInt32>())
                sum += item;
            return sum;
        }
    }
}
