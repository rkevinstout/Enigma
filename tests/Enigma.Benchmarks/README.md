```

BenchmarkDotNet v0.13.12, macOS 26.3.1 (25D2128) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.103
  [Host]     : .NET 10.0.3 (10.0.326.7603), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 10.0.3 (10.0.326.7603), Arm64 RyuJIT AdvSIMD


```
| Method  | Length | Mean      | Error    | StdDev   | Gen0   | Allocated |
|-------- |------- |----------:|---------:|---------:|-------:|----------:|
| **Encrpyt** | **256**    |  **14.95 μs** | **0.074 μs** | **0.062 μs** | **0.1678** |   **1.09 KB** |
| **Encrpyt** | **1024**   |  **59.37 μs** | **0.334 μs** | **0.279 μs** | **0.6104** |   **4.09 KB** |
| **Encrpyt** | **4096**   | **243.21 μs** | **1.632 μs** | **1.446 μs** | **2.4414** |  **16.09 KB** |
