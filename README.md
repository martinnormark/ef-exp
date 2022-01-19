# Entity Framework Core - Performance experiments

## Bulk Deleting entities

Should you load up the entities you want to delete with tracking? With no tracking? Or only select the IDs, then attach a shallow object with ID and call `RemoveRange`?

## Tiny entity with two properties

Testing with a small model, that consists of only `Id` and a `Sequence` string property, we can see that it almost doesn't make a difference.

```ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-YOJEMN : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

InvocationCount=1  UnrollFactor=1

```

| Method                   | IdsAbove  |          Mean |         Error |        StdDev |        Median |
| ------------------------ | --------- | ------------: | ------------: | ------------: | ------------: |
| **DeleteFullEntity**     | **100**   | **520.70 ms** | **30.551 ms** | **88.148 ms** | **484.33 ms** |
| DeleteFullEntityDetached | 100       |     608.28 ms |     21.950 ms |     64.376 ms |     574.48 ms |
| DeleteByIdEntityDetached | 100       |     548.46 ms |      8.075 ms |      7.158 ms |     548.49 ms |
| **DeleteFullEntity**     | **8000**  | **245.93 ms** |  **4.036 ms** |  **3.371 ms** | **245.00 ms** |
| DeleteFullEntityDetached | 8000      |     283.42 ms |     10.297 ms |     30.038 ms |     268.60 ms |
| DeleteByIdEntityDetached | 8000      |     257.17 ms |      5.057 ms |     10.666 ms |     255.10 ms |
| **DeleteFullEntity**     | **13500** |  **52.39 ms** |  **1.833 ms** |  **5.109 ms** |  **50.83 ms** |
| DeleteFullEntityDetached | 13500     |      55.94 ms |      2.205 ms |      6.220 ms |      53.27 ms |
| DeleteByIdEntityDetached | 13500     |      46.98 ms |      2.169 ms |      6.009 ms |      44.52 ms |
