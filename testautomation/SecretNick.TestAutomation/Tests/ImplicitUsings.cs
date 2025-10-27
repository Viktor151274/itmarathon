// Specifies that the test fixtures can run in parallel
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
// Sets the maximum degree of parallelism to 4, allowing up to 4 test fixtures to run concurrently
[assembly: LevelOfParallelism(4)]
