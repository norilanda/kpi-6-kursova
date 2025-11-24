# Solution for Detecting flaky tests automatically.

We decided to use C# with xUnit test framework

Our tool works in the next way:
1. We run the xUnit tests via command line with specifying the log file path: `dotnet test --logger "trx;LogFileName=test-results.trx"`
2. We repeat it a several time
3. We take the outputs and compare them with the created tool which shows which tests may be flaky depending on their results in every run (if in one run a test passed but in another one it fails - it is flaky, if it passes/fails in every run - it is considered as NOT flaky)

The project the tool was tested on: https://github.com/norilanda/kpi-6-kursova/tree/dev

