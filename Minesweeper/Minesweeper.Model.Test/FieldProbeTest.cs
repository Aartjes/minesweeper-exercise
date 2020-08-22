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
            yield return new TestCaseData(new Field(new[,] { { false } }), 0, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_BlankField_0_0_Returns_Zero");
            yield return new TestCaseData(new Field (new[,] { { true } }), 0, 0, FieldProbeResult.Mine)
                .SetName("TestProbeResult_JustMine_0_0_Returns_Mine");
            
            var field = new Field(new[,]
            {
                { true, false, false},
                { false, false, false},
                { false, false, false},
            });
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

            field = new Field(new[,]
            {
                { false, false, false },
                { false, false, false },
                { false, false, true },
            });
            yield return new TestCaseData(field, 0, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomRight_0_0_Returns_Zero");
            yield return new TestCaseData(field, 1, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomRight_1_0_Returns_Zero");
            yield return new TestCaseData(field, 0, 1, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomRight_0_1_Returns_Zero");
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3BottomRight_1_1_Returns_One");
            yield return new TestCaseData(field, 2, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomRight_2_0_Returns_Zero");
            yield return new TestCaseData(field, 2, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3BottomRight_2_1_Returns_One");
            yield return new TestCaseData(field, 2, 2, FieldProbeResult.Mine)
                .SetName("TestProbeResult_3x3BottomRight_2_2_Returns_Mine");
            yield return new TestCaseData(field, 1, 2, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3BottomRight_1_2_Returns_One");
            yield return new TestCaseData(field, 0, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomRight_0_2_Returns_Zero");



            field = new Field(new[,]
            {
                { false, false, false },
                { false, false, false },
                { true, false, false },
            });
            yield return new TestCaseData(field, 0, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopRight_0_0_Returns_Zero");
            yield return new TestCaseData(field, 1, 0, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3TopRight_1_0_Returns_One");
            yield return new TestCaseData(field, 0, 1, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopRight_0_1_Returns_Zero");
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3TopRight_1_1_Returns_One");
            yield return new TestCaseData(field, 2, 0, FieldProbeResult.Mine)
                .SetName("TestProbeResult_3x3TopRight_2_0_Returns_Mine");
            yield return new TestCaseData(field, 2, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3TopRight_2_1_Returns_One");
            yield return new TestCaseData(field, 2, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopRight_2_2_Returns_Zero");
            yield return new TestCaseData(field, 1, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopRight_1_2_Returns_Zero");
            yield return new TestCaseData(field, 0, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3TopRight_0_2_Returns_Zero");

            field = new Field(new[,]
            {
                { false, false, true },
                { false, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 0, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomLeft_0_0_Returns_Zero");
            yield return new TestCaseData(field, 1, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomLeft_1_0_Returns_Zero");
            yield return new TestCaseData(field, 0, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3BottomLeft_0_1_Returns_One");
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3BottomLeft_1_1_Returns_One");
            yield return new TestCaseData(field, 2, 0, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomLeft_2_0_Returns_Zero");
            yield return new TestCaseData(field, 2, 1, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomLeft_2_1_Returns_Zero");
            yield return new TestCaseData(field, 2, 2, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3BottomLeft_2_2_Returns_Zero");
            yield return new TestCaseData(field, 1, 2, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3BottomLeft_1_2_Returns_One");
            yield return new TestCaseData(field, 0, 2, FieldProbeResult.Mine)
                .SetName("TestProbeResult_3x3BottomLeft_0_2_Returns_Mine");


            field = new Field(new[,]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3NoMines_1_1_Returns_Zero");

            field = new Field(new[,]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Zero)
                .SetName("TestProbeResult_3x3_0Mines_1_1_Returns_Zero");
            field = new Field(new[,]
            {
                { true, false, false },
                { false, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.One)
                .SetName("TestProbeResult_3x3_1Mine_1_1_Returns_One");
            field = new Field(new[,]
            {
                { true, true, false },
                { false, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Two)
                .SetName("TestProbeResult_3x3_2Mines_1_1_Returns_Two");
            field = new Field(new[,]
            {
                { true, true, true },
                { false, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Three)
                .SetName("TestProbeResult_3x3_3Mines_1_1_Returns_Three");
            field = new Field(new[,]
            {
                { true, true, true },
                { true, false, false },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Four)
                .SetName("TestProbeResult_3x3_4Mines_1_1_Returns_Four");
            field = new Field(new[,]
            {
                { true, true, true },
                { true, false, true },
                { false, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Five)
                .SetName("TestProbeResult_3x3_5Mines_1_1_Returns_Five");
            field = new Field(new[,]
            {
                { true, true, true },
                { true, false, true },
                { true, false, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Six)
                .SetName("TestProbeResult_3x3_6Mines_1_1_Returns_Six");
            field = new Field(new[,]
            {
                { true, true, true },
                { true, false, true },
                { true, true, false },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Seven)
                .SetName("TestProbeResult_3x3_7Mines_1_1_Returns_Seven");
            field = new Field(new[,]
            {
                { true, true, true },
                { true, false, true },
                { true, true, true },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Eight)
                .SetName("TestProbeResult_3x3_8Mines_1_1_Returns_Eight");
            field = new Field(new[,]
            {
                { true, true, true },
                { true, true, true },
                { true, true, true },
            });
            yield return new TestCaseData(field, 1, 1, FieldProbeResult.Mine)
                .SetName("TestProbeResult_3x3_9Mines_1_1_Returns_Mine");
        }

        [TestCaseSource("TestProbeResultCases")]
        public void TestProbeResult(Field field, int x, int y, FieldProbeResult expectedResult)
        {
            var result = _probe.Probe(field, x, y);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
