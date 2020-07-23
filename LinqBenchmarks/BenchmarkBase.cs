﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;

namespace LinqBenchmarks
{
    //[SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    //[SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    //[RPlotExporter, CsvMeasurementsExporter] // requires installation of R (https://benchmarkdotnet.org/articles/configs/exporters.html#plots)
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)]
    public class BenchmarkBase
    {
        [Params(0, 1, 10, 100)]
        public int Count { get; set; }
    }
}
