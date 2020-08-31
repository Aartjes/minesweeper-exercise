using Com.Github.Aartjes.Minesweeper.Cli;
using Com.Github.Aartjes.Minesweeper.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Com.Gitlab.Aartjes.Minesweeper.Cli.Test
{
    public class TestGameStatePrinterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(10, 10)]
        [TestCase(20, 30)]
        public void Print_CallsStringConverterForEverySpace(int width, int height)
        {
            int convertorCount = 0;
            var convertorMock = new Mock<ISpaceStateToStringConvertor>();
            var stateMock = new Mock<IGameState>();
            stateMock.Setup(state => state.FieldWidth)
                .Returns(width);
            stateMock.Setup(state => state.FieldHeight)
                .Returns(height);
            convertorMock.Setup(convertor => convertor.Convert(It.IsAny<GameSpaceState>()))
                .Returns(".")
                .Callback(() => convertorCount += 1);
            var printer = new GameStatePrinter(convertorMock.Object);

            printer.Print(stateMock.Object);

            Assert.AreEqual(width * height, convertorCount);
        }

        [TestCaseSource(nameof(TestPrintResultCases))]
        public void TestPrintResult(GameState state, string expectedResult)
        {
            var printer = new GameStatePrinter(new SpaceStateToStringConvertor());
            var result = printer.Print(state);
            Assert.AreEqual(result, expectedResult);
        }

        public static IEnumerable<TestCaseData> TestPrintResultCases()
        {
            var state = new GameState(
                new Field(new[,] { { false } }),
                new FieldProbe(),
                new SpaceStateConvertor());
            var expectedResult =
                "\t|\t1\r\n"
                + "-\t-\t-\r\n"
                + "1\t|\t.\r\n";
            yield return new TestCaseData(state, expectedResult).SetName("TestPrintResult_1UnsteppedSpace");

            state = new GameState(
                new Field(new bool[3,2]),
                new FieldProbe(),
                new SpaceStateConvertor());
            expectedResult =
                "\t|\t1\t2\t3\r\n"
                + "-\t-\t-\t-\t-\r\n"
                + "1\t|\t.\t.\t.\r\n"
                + "2\t|\t.\t.\t.\r\n";
            yield return new TestCaseData(state, expectedResult).SetName("TestPrintResult_3x2UnsteppedSpaces");

            var fieldArray = new bool[5, 5];
            fieldArray[2, 2] = true;
            state = new GameState(
                new Field(fieldArray),
                new FieldProbe(),
                new SpaceStateConvertor());
            expectedResult =
                "\t|\t1\t2\t3\t4\t5\r\n"
                + "-\t-\t-\t-\t-\t-\t-\r\n"
                + "1\t|\t.\t.\t.\t.\t.\r\n"
                + "2\t|\t.\t.\t.\t.\t.\r\n"
                + "3\t|\t.\t.\t.\t.\t.\r\n"
                + "4\t|\t.\t.\t.\t.\t.\r\n"
                + "5\t|\t.\t.\t.\t.\t.\r\n";
            yield return new TestCaseData(state, expectedResult).SetName("TestPrintResult_5x5UnsteppedSpaces");

            fieldArray = new bool[5, 5];
            fieldArray[2, 2] = true;
            state = new GameState(
                new Field(fieldArray),
                new FieldProbe(),
                new SpaceStateConvertor());
            state.ToggleFlag(2, 2);
            new StepCommand(0, 0).Execute(state);
            expectedResult =
                "\t|\t1\t2\t3\t4\t5\r\n"
                + "-\t-\t-\t-\t-\t-\t-\r\n"
                + "1\t|\t0\t0\t0\t0\t0\r\n"
                + "2\t|\t0\t1\t1\t1\t0\r\n"
                + "3\t|\t0\t1\tP\t1\t0\r\n"
                + "4\t|\t0\t1\t1\t1\t0\r\n"
                + "5\t|\t0\t0\t0\t0\t0\r\n";
            yield return new TestCaseData(state, expectedResult).SetName("TestPrintResult_5x5SteppedAndFlagged");

            fieldArray = new bool[5, 5];
            fieldArray[2, 2] = true;
            state = new GameState(
                new Field(fieldArray),
                new FieldProbe(),
                new SpaceStateConvertor());
            state.Step(2, 2);
            expectedResult =
                "\t|\t1\t2\t3\t4\t5\r\n"
                + "-\t-\t-\t-\t-\t-\t-\r\n"
                + "1\t|\t.\t.\t.\t.\t.\r\n"
                + "2\t|\t.\t.\t.\t.\t.\r\n"
                + "3\t|\t.\t.\t*\t.\t.\r\n"
                + "4\t|\t.\t.\t.\t.\t.\r\n"
                + "5\t|\t.\t.\t.\t.\t.\r\n";
            yield return new TestCaseData(state, expectedResult).SetName("TestPrintResult_5x5SteppedOnMine");
        }
    }
}