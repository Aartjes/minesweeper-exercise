using Com.Github.Aartjes.Minesweeper.Cli;
using Com.Github.Aartjes.Minesweeper.Model;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Reflection.PortableExecutable;

namespace Com.Gitlab.Aartjes.Minesweeper.Cli.Test
{
    public class CommandInterpreterTest
    {
        private Mock<IProgram> _programMock;
        private CommandInterpreter _interpreter;

        [SetUp]
        public void Setup()
        {
            _programMock = new Mock<IProgram>();
            _interpreter = new CommandInterpreter();
        }

        [TestCase("step 1 1", 0, 0)]
        [TestCase("stEP 1,1", 0, 0)]
        [TestCase("   stEP   1,   \t      1", 0, 0)]
        [TestCase("step 12 12", 11, 11)]
        [TestCase("step 23 42", 22, 41)]
        public void TestStepCommand(string command, int x, int y)
        {
            IGameCommand executedCommand = null;
            var gameMock = new Mock<IGame>();
            gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Callback((IGameCommand gameCommand) => executedCommand = gameCommand);
            _programMock.Setup(program => program.Game)
                .Returns(gameMock.Object);
            
            _interpreter.Interpret(command, _programMock.Object);

            var stepCommand = (StepCommand)executedCommand;
            Assert.AreEqual(x, stepCommand.X);
            Assert.AreEqual(y, stepCommand.Y);
        }

        [TestCase("flag 1 1", 0, 0)]
        [TestCase("FlAg 1,1", 0, 0)]
        [TestCase("   FlaG   1,   \t      1", 0, 0)]
        [TestCase("flag 12 12", 11, 11)]
        [TestCase("flag 23 42", 22, 41)]
        public void TestFlagCommand(string command, int x, int y)
        {
            IGameCommand executedCommand = null;
            var gameMock = new Mock<IGame>();
            gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Callback((IGameCommand gameCommand) => executedCommand = gameCommand);
            _programMock.Setup(program => program.Game)
                .Returns(gameMock.Object);

            _interpreter.Interpret(command, _programMock.Object);

            var flagCommand = (FlagCommand)executedCommand;
            Assert.AreEqual(x, flagCommand.X);
            Assert.AreEqual(y, flagCommand.Y);
        }

        [TestCase("exit")]
        [TestCase("Exit")]
        [TestCase("exIT")]
        [TestCase("  exit   \t,")]
        public void Exit_CallsProgramExit(string command)
        {
            int amountOfTimesExitHasBeenCalled = 0;
            _programMock.Setup(program => program.Exit())
                .Callback(() => amountOfTimesExitHasBeenCalled += 1);

            _interpreter.Interpret(command, _programMock.Object);

            Assert.AreEqual(1, amountOfTimesExitHasBeenCalled);
        }

        [TestCase(GameStatus.Win, 0, 1)]
        [TestCase(GameStatus.Loss, 1, 0)]
        public void TestWinLoss_CallsProgramWinLose(GameStatus status, int expectedLoseCalls, int expectedWinCalls)
        {
            int amountOfTimesLoseIsCalled = 0;
            _programMock.Setup(program => program.Lose())
                .Callback(() => amountOfTimesLoseIsCalled += 1);

            int amountOfTimesWinIsCalled = 0;
            _programMock.Setup(program => program.Win())
                .Callback(() => amountOfTimesWinIsCalled += 1);

            var gameMock = new Mock<IGame>();
            gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Returns(status);

            _programMock.Setup(program => program.Game)
                .Returns(gameMock.Object);

            var command = "step 1, 1";

            _interpreter.Interpret(command, _programMock.Object);

            Assert.AreEqual(expectedLoseCalls, amountOfTimesLoseIsCalled);
            Assert.AreEqual(expectedWinCalls, amountOfTimesWinIsCalled);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("sglkhdfglksdhfg")]
        [TestCase("This isn't an existing command.")]
        public void Interpret_EmptyNullOrGibberishCommand_AskAgain(string command)
        {
            int amountOfTimesAsked = 0;
            _programMock.Setup(program => program.AskForCommand())
                .Callback(() => amountOfTimesAsked += 1);

            _interpreter.Interpret(command, _programMock.Object);

            Assert.AreEqual(1, amountOfTimesAsked);
        }

        [TestCase("Yes", 1, 0)]
        [TestCase("No", 0, 1)]
        [TestCase("  YeS  ,", 1, 0)]
        [TestCase(" , nO ,", 0, 1)]
        public void InterpretNewGameYesNo_YesCallsProgramNewGame(string command, int expectedNewGameCallCounts, int expectedExitCounts)
        {
            int newGameCallCount = 0;
            _programMock.Setup(program => program.NewGame())
                .Callback(() => newGameCallCount += 1);
            int exitCounts = 0;
            _programMock.Setup(program => program.Exit())
                .Callback(() => exitCounts += 1);

            _interpreter.InterpretNewGameYesNo(command, _programMock.Object);

            Assert.AreEqual(expectedNewGameCallCounts, newGameCallCount);
            Assert.AreEqual(expectedExitCounts, exitCounts);
        }

        [TestCase(null)]
        [TestCase("")]
        public void InterpretNewGameYesNo_NullOrEmpty_AskAgain(string response)
        {
            int amountOfTimesAsked = 0;
            _programMock.Setup(program => program.AskForNewGame())
                .Callback(() => amountOfTimesAsked += 1);

            _interpreter.InterpretNewGameYesNo(response, _programMock.Object);

            Assert.AreEqual(1, amountOfTimesAsked);
        }
    }
}
