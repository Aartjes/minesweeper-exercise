using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class SpaceStateConvertorTest
    {
        [TestCase(FieldProbeResult.Mine, GameSpaceState.Mine, TestName = "TestConversion_Mine")]
        [TestCase(FieldProbeResult.Zero, GameSpaceState.Zero, TestName = "TestConversion_Zero")]
        [TestCase(FieldProbeResult.One, GameSpaceState.One, TestName = "TestConversion_One")]
        [TestCase(FieldProbeResult.Two, GameSpaceState.Two, TestName = "TestConversion_Two")]
        [TestCase(FieldProbeResult.Three, GameSpaceState.Three, TestName = "TestConversion_Three")]
        [TestCase(FieldProbeResult.Four, GameSpaceState.Four, TestName = "TestConversion_Four")]
        [TestCase(FieldProbeResult.Five, GameSpaceState.Five, TestName = "TestConversion_Five")]
        [TestCase(FieldProbeResult.Six, GameSpaceState.Six, TestName = "TestConversion_Six")]
        [TestCase(FieldProbeResult.Seven, GameSpaceState.Seven, TestName = "TestConversion_Seven")]
        [TestCase(FieldProbeResult.Eight, GameSpaceState.Eight, TestName = "TestConversion_Eight")]
        public void TestConversion(FieldProbeResult spaceState, GameSpaceState expectedGameSpaceState)
        {
            var convertor = new SpaceStateConvertor();

            var result = convertor.Convert(spaceState);

            Assert.AreEqual(expectedGameSpaceState, result);
        }
    }
}
