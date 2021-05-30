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
            //string text = "0123456789";
            //string pattern = "23";
            //Stopwatch stopwatch = new Stopwatch();

            //stopwatch.Start();
            //var (result1, comparisons) = Algorithms.KMP(text, pattern);
            //stopwatch.Stop();

            //Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

            var inputs = ReadWrite.ReadFile("testFile1.txt");

            Stopwatch KMPStopW = new Stopwatch();
            Stopwatch naiveStopW = new Stopwatch();

            var KMPResults = new List<(List<int>, int)>();
            var naiveResults = new List<(List<int>, int)>();

            KMPStopW.Start();
            foreach (var instance in inputs)
                KMPResults.Add(Algorithms.KMP(instance.Item1, instance.Item2));
            KMPStopW.Stop();

            naiveStopW.Start();
            foreach (var instance in inputs)
                naiveResults.Add(Algorithms.NaiveAglorithm(instance.Item1, instance.Item2));
            naiveStopW.Stop();

            double avgKMP = 0, avgNaive = 0;
            foreach (var res in KMPResults)
                avgKMP += res.Item2;
            avgKMP /= inputs.Count;
            foreach (var res in naiveResults)
                avgNaive += res.Item2;
            avgNaive /= inputs.Count;

            using (StreamWriter sw = File.CreateText("test1output.txt"))
            {
                sw.WriteLine($"{KMPStopW.ElapsedMilliseconds}:{naiveStopW.ElapsedMilliseconds}");
                sw.WriteLine($"{avgKMP}:{avgNaive}");
                for (int i = 0; i < inputs.Count; i++)
                    sw.WriteLine($"{KMPResults[i].Item2}:{naiveResults[i].Item2}");
            }

            Assert.IsTrue(avgKMP < avgNaive);
        }

        [TestMethod]
        public void TestMethod2()
        {

        }
    }
}
