using Com.Github.Aartjes.Minesweeper.Cli;
using Com.Github.Aartjes.Minesweeper.Model;
using NUnit.Framework;

namespace Com.Gitlab.Aartjes.Minesweeper.Cli.Test
{
    public class SpaceStateToStringConvertorTest
    {
        [TestCase(GameSpaceState.Blank, ".")]
        [TestCase(GameSpaceState.Flag,  "P")]
        [TestCase(GameSpaceState.Mine,  "*")]
        [TestCase(GameSpaceState.Zero,  "0")]
        [TestCase(GameSpaceState.One,   "1")]
        [TestCase(GameSpaceState.Two,   "2")]
        [TestCase(GameSpaceState.Three, "3")]
        [TestCase(GameSpaceState.Four,  "4")]
        [TestCase(GameSpaceState.Five,  "5")]
        [TestCase(GameSpaceState.Six,   "6")]
        [TestCase(GameSpaceState.Seven, "7")]
        [TestCase(GameSpaceState.Eight, "8")]
        public void TestSpaceStateToStringConversion(GameSpaceState input, string expectedOutput)
        {
            var convertor = new SpaceStateToStringConvertor();
            var output = convertor.Convert(input);
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
