using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class FieldProbeResultTest
    {
        [TestCase(-1, FieldProbeResult.Mine, TestName = "TestIntConversion_-1_Becomes_Mine")]
        [TestCase(0, FieldProbeResult.Zero, TestName = "TestIntConversion_0_Becomes_Mine")]
        [TestCase(1, FieldProbeResult.One, TestName = "TestIntConversion_1_Becomes_One")]
        [TestCase(2, FieldProbeResult.Two, TestName = "TestIntConversion_2_Becomes_Two")]
        [TestCase(3, FieldProbeResult.Three, TestName = "TestIntConversion_3_Becomes_Three")]
        [TestCase(4, FieldProbeResult.Four, TestName = "TestIntConversion_4_Becomes_Four")]
        [TestCase(5, FieldProbeResult.Five, TestName = "TestIntConversion_5_Becomes_Five")]
        [TestCase(6, FieldProbeResult.Six, TestName = "TestIntConversion_6_Becomes_Six")]
        [TestCase(7, FieldProbeResult.Seven, TestName = "TestIntConversion_7_Becomes_Seven")]
        [TestCase(8, FieldProbeResult.Eight, TestName = "TestIntConversion_8_Becomes_Eight")]
        public void TestIntConversion(int intValue, FieldProbeResult expectedState)
        {
            Assert.AreEqual(expectedState, (FieldProbeResult)intValue);
        }

        [TestCase(-1, FieldProbeResult.Mine, TestName = "TestBackToIntConversion_-1_From_Mine")]
        [TestCase(0, FieldProbeResult.Zero, TestName = "TestBackToIntConversion_0_From_Mine")]
        [TestCase(1, FieldProbeResult.One, TestName = "TestBackToIntConversion_1_From_One")]
        [TestCase(2, FieldProbeResult.Two, TestName = "TestBackToIntConversion_2_From_Two")]
        [TestCase(3, FieldProbeResult.Three, TestName = "TestBackToIntConversion_3_From_Three")]
        [TestCase(4, FieldProbeResult.Four, TestName = "TestBackToIntConversion_4_From_Four")]
        [TestCase(5, FieldProbeResult.Five, TestName = "TestBackToIntConversion_5_From_Five")]
        [TestCase(6, FieldProbeResult.Six, TestName = "TestBackToIntConversion_6_From_Six")]
        [TestCase(7, FieldProbeResult.Seven, TestName = "TestBackToIntConversion_7_From_Seven")]
        [TestCase(8, FieldProbeResult.Eight, TestName = "TestBackToIntConversion_8_From_Eight")]
        public void TestBackToIntConversion(int expectedInt, FieldProbeResult state)
        {
            Assert.AreEqual(expectedInt, (int)state);
        }
    }
}
