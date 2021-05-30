using System;
using System.Diagnostics;
using AZ_KMP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace AZ_KMP_Tests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var inputs = ReadWrite.ReadFile("testFile1.txt");

            Stopwatch KMPStopW = new Stopwatch();
            Stopwatch naiveStopW = new Stopwatch();

            var KMPResults = new List<(List<int>, int, long)>();
            var naiveResults = new List<(List<int>, int, long)>();

            foreach (var instance in inputs)
            {
                KMPStopW.Start();
                var KMPres = Algorithms.KMP(instance.Item1, instance.Item2);
                KMPStopW.Stop();
                KMPResults.Add((KMPres.Item1, KMPres.Item2, KMPStopW.ElapsedTicks));
                KMPStopW.Reset();
            }

            foreach (var instance in inputs)
            {
                naiveStopW.Start();
                var naiveres = Algorithms.NaiveAlgorithm(instance.Item1, instance.Item2);
                naiveStopW.Stop();
                naiveResults.Add((naiveres.Item1, naiveres.Item2, naiveStopW.ElapsedTicks));
                naiveStopW.Reset();
            }

            double avgKMP = 0, avgNaive = 0;
            foreach (var res in KMPResults)
                avgKMP += res.Item2;
            avgKMP /= inputs.Count;
            foreach (var res in naiveResults)
                avgNaive += res.Item2;
            avgNaive /= inputs.Count;

            using (StreamWriter sw = File.CreateText("test1output.txt"))
            {
                sw.WriteLine("inputTextLength:inputPatternLength:patternOccurences:KMPComparisons:KMPTime:KMPOccurences:naiveComparisons:naiveTime:naiveOccurences");
                //sw.WriteLine($"{KMPStopW.ElapsedMilliseconds}:{naiveStopW.ElapsedMilliseconds}");
                //sw.WriteLine($"{avgKMP}:{avgNaive}");
                for (int i = 0; i < inputs.Count; i++)
                    sw.WriteLine($"{inputs[i].Item1.Length}:{inputs[i].Item2.Length}:{inputs[i].Item3}:{KMPResults[i].Item2}:{KMPResults[i].Item3}:{KMPResults[i].Item1.Count}:{naiveResults[i].Item2}:{naiveResults[i].Item3}:{naiveResults[i].Item1.Count}");
            }

            Assert.IsTrue(avgKMP < avgNaive);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var inputs = ReadWrite.ReadFile("testFile2.txt");

            Stopwatch tableStopW = new Stopwatch();
            Stopwatch KMPStopW = new Stopwatch();
            Stopwatch KMP2StopW = new Stopwatch();
            Stopwatch naiveStopW = new Stopwatch();

            var KMPResults = new List<(List<int>, int, long)>();
            var KMP2Results = new List<(List<int>, int, long)>();
            var naiveResults = new List<(List<int>, int, long)>();

            long KMPtime = 0, naivetime = 0;

            tableStopW.Start();
            var resTable = Algorithms.GetTable("abcab");
            tableStopW.Stop();
            KMPtime += tableStopW.ElapsedTicks;

            foreach (var instance in inputs)
            {
                KMPStopW.Start();
                var KMPres = Algorithms.KMPKnownTable(instance.Item1, instance.Item2, resTable.Item1);
                KMPStopW.Stop();

                KMP2StopW.Start();
                var KMP2res = Algorithms.KMP(instance.Item1, instance.Item2);
                KMP2StopW.Stop();

                if (KMPResults.Count == 0)
                    KMPResults.Add((KMPres.Item1, KMPres.Item2, KMPStopW.ElapsedTicks + tableStopW.ElapsedTicks));
                else
                    KMPResults.Add((KMPres.Item1, KMPres.Item2, KMPStopW.ElapsedTicks));
                KMPtime += KMPStopW.ElapsedTicks;

                KMP2Results.Add((KMP2res.Item1, KMP2res.Item2, KMP2StopW.ElapsedTicks));
                
                KMPStopW.Reset();
                KMP2StopW.Reset();
            }

            foreach (var instance in inputs)
            {
                naiveStopW.Start();
                var naiveres = Algorithms.NaiveAlgorithm(instance.Item1, instance.Item2);
                naiveStopW.Stop();
                naiveResults.Add((naiveres.Item1, naiveres.Item2, naiveStopW.ElapsedTicks));
                naivetime += naiveStopW.ElapsedTicks;
                naiveStopW.Reset();
            }

            using (StreamWriter sw = File.CreateText("test2output.txt"))
            {
                sw.WriteLine("inputTextLength:inputPatternLength:patternOccurences:KMPComparisons:KMPTime:KMP2Time:KMPOccurences:naiveComparisons:naiveTime:naiveOccurences");
                //sw.WriteLine($"{KMPStopW.ElapsedMilliseconds}:{naiveStopW.ElapsedMilliseconds}");
                //sw.WriteLine($"{avgKMP}:{avgNaive}");
                for (int i = 0; i < inputs.Count; i++)
                    sw.WriteLine($"{inputs[i].Item1.Length}:{inputs[i].Item2.Length}:{inputs[i].Item3}:{KMPResults[i].Item2}:{KMPResults[i].Item3}:{KMP2Results[i].Item3}:{KMPResults[i].Item1.Count}:{naiveResults[i].Item2}:{naiveResults[i].Item3}:{naiveResults[i].Item1.Count}");
            }

            Assert.IsTrue(KMPtime > naivetime);
        }
    }
}
