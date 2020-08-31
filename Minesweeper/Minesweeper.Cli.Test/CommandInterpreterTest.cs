using Com.Github.Aartjes.Minesweeper.Cli;
using Com.Github.Aartjes.Minesweeper.Model;
using Moq;
using NUnit.Framework;

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
            _interpreter = new CommandInterpreter(_programMock.Object);
        }

        [TestCase("step 0 0", 0, 0)]
        [TestCase("stEP 0,0", 0, 0)]
        [TestCase("   stEP   0,   \t      0", 0, 0)]
        [TestCase("step 11 11", 11, 11)]
        [TestCase("step 22 41", 22, 41)]
        public void TestStepCommand(string command, int x, int y)
        {
            IGameCommand executedCommand = null;
            var gameMock = new Mock<IGame>();
            gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Callback((IGameCommand gameCommand) => executedCommand = gameCommand);
            _programMock.Setup(program => program.Game)
                .Returns(gameMock.Object);
            
            _interpreter.Interpret(command);

            var stepCommand = (StepCommand)executedCommand;
            Assert.AreEqual(x, stepCommand.X);
            Assert.AreEqual(y, stepCommand.Y);
        }

        [TestCase("flag 0 0", 0, 0)]
        [TestCase("FlAg 0,0", 0, 0)]
        [TestCase("   FlaG   0,   \t      0", 0, 0)]
        [TestCase("flag 11 11", 11, 11)]
        [TestCase("flag 22 41", 22, 41)]
        public void TestFlagCommand(string command, int x, int y)
        {
            IGameCommand executedCommand = null;
            var gameMock = new Mock<IGame>();
            gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Callback((IGameCommand gameCommand) => executedCommand = gameCommand);
            _programMock.Setup(program => program.Game)
                .Returns(gameMock.Object);

            _interpreter.Interpret(command);

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

            _interpreter.Interpret(command);

            Assert.AreEqual(1, amountOfTimesExitHasBeenCalled);
        }
    }
}
