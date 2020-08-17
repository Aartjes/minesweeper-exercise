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
            yield return new TestCaseData(new[,] { { false } }, 0, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_BlankField_0_0_Returns_Zero");
            yield return new TestCaseData(new[,] { { true } }, 0, 0, FieldProbeResult.Mine)
                .SetName("TestProbeResult_JustMine_0_0_Returns_Mine");
            
            var field = new[,]
            {
                { true, false, false},
                { false, false, false},
                { false, false, false},
            };
            yield return new TestCaseData(field, 0, 0, FieldProbeResult.Mine)
                .SetName("TestProbeResult_3x3TopLeft_0_0_Returns_Mine");
            yield return new TestCaseData(field, 1, 0, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3TopLeft_1_0_Returns_One");
            yield return new TestCaseData(field, 0, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3TopLeft_0_1_Returns_One");
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3TopLeft_1_1_Returns_One");
            yield return new TestCaseData(field, 2, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopLeft_2_0_Returns_Zero");
            yield return new TestCaseData(field, 2, 1, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopLeft_2_1_Returns_Zero");
            yield return new TestCaseData(field, 2, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopLeft_2_2_Returns_Zero");
            yield return new TestCaseData(field, 1, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopLeft_1_2_Returns_Zero");
            yield return new TestCaseData(field, 0, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopLeft_0_2_Returns_Zero");
        }

        [TestCaseSource("TestProbeResultCases")]
        public void TestProbeResult(bool[,] field, int x, int y, FieldProbeResult expectedResult)
        {
            var result = _probe.Probe(field, x, y);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
