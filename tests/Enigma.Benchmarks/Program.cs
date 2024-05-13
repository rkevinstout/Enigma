using BenchmarkDotNet.Running;
using Enigma.Benchmarks;

var summary = BenchmarkRunner.Run<CaesarShiftTests>();