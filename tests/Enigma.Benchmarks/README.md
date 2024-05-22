```

BenchmarkDotNet v0.13.12, macOS Sonoma 14.4.1 (23E224) [Darwin 23.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.300
  [Host]     : .NET 8.0.5 (8.0.524.21615), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.5 (8.0.524.21615), Arm64 RyuJIT AdvSIMD


```
| Method  | Length | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|-------- |------- |----------:|---------:|---------:|--------:|-------:|----------:|
| **Encrpyt** | **256**    |  **22.76 μs** | **0.079 μs** | **0.066 μs** |  **3.1738** |      **-** |  **19.62 KB** |
| **Encrpyt** | **1024**   |  **90.19 μs** | **0.235 μs** | **0.196 μs** | **12.5732** |      **-** |  **77.48 KB** |
| **Encrpyt** | **4096**   | **360.18 μs** | **1.282 μs** | **1.136 μs** | **50.2930** | **0.9766** |  **308.5 KB** |
