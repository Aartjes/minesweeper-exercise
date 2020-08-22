using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class FieldTest
    {
        [TestCaseSource("FieldDimensionTest_Cases")]
        public void FieldDimensionTest(Field field, int expectedWidth, int expectedHeight, int expectedNumberOfMines)
        {
            Assert.AreEqual(expectedWidth, field.Width);
            Assert.AreEqual(expectedHeight, field.Height);
            Assert.AreEqual(expectedNumberOfMines, field.MineCount);
        }

        public static IEnumerable<TestCaseData> FieldDimensionTest_Cases()
        {
            var fieldArray = new bool[10, 5];
            fieldArray[0, 0] = true;
            fieldArray[6, 4] = true;
            fieldArray[5, 3] = true;
            fieldArray[0, 4] = true;

            yield return new TestCaseData(new Field(fieldArray), 10, 5, 4)
                .SetName("FieldDimensionTest_10x5_4Mines");
            fieldArray = new bool[100, 100];
            fieldArray[0, 0] = true;
            fieldArray[6, 4] = true;
            fieldArray[5, 3] = true;
            fieldArray[0, 4] = true;
            fieldArray[20, 0] = true;
            fieldArray[50, 4] = true;
            fieldArray[5,60] = true;
            fieldArray[0, 80] = true;

            yield return new TestCaseData(new Field(fieldArray), 100, 100, 8)
                .SetName("FieldDimensionTest_100x100_8Mines");
        }
    }
}
