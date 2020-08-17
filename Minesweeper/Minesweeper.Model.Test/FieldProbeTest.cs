using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class FieldProbeTest
    {
        private FieldProbe _probe;

        [SetUp]
        public void Setup()
        {
            _probe = new FieldProbe();
        }

        public static IEnumerable<TestCaseData> TestProbeResultCases()
        {
            var data = new TestCaseData(new[,] { { false } }, 0, 0, FieldProbeResult.Zero);
            yield return data;
        }

        [TestCaseSource("TestProbeResultCases")]
        public void TestProbeResult(bool[,] field, int x, int y, FieldProbeResult expectedResult)
        {
            var result = _probe.Probe(field, x, y);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
