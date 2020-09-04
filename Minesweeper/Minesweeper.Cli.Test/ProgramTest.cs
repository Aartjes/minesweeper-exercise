using Com.Github.Aartjes.Minesweeper.Cli;
using Com.Github.Aartjes.Minesweeper.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Com.Gitlab.Aartjes.Minesweeper.Cli.Test
{
    public class ProgramTest
    {
        private Mock<IGame> _gameMock;
        private Mock<IGameFactory> _gameFactoryMock;
        private Mock<ICommunicator> _communicatorMock;
        private Mock<IGameStatePrinter> _gameStatePrinterMock;
        private Program _program;

        [SetUp]
        public void SetUp()
        {
            _gameMock = new Mock<IGame>();
            _gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Returns(GameStatus.Ongoing);
            _gameFactoryMock = new Mock<IGameFactory>();
            _gameFactoryMock.Setup(factory => factory.Create())
                .Returns(_gameMock.Object);
            _communicatorMock = new Mock<ICommunicator>();
            _gameStatePrinterMock = new Mock<IGameStatePrinter>();
            _program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object);
        }

        [Test]
        public void ExecutionStopsAfterCallingExit()
        {
            var interpreterMock = new Mock<ICommandInterpreter>();
            interpreterMock.Setup(mock => mock.Interpret(It.IsAny<string>()))
                .Callback(() => _program.Exit());
            _program.Execute(interpreterMock.Object);

            //if the program has stopped it reaches here, otherwise it;s still in a loop.
            Assert.Pass();
        }

        [TestCase("Exit")]
        [TestCase("Step 1,1", "Exit")]
        [TestCase("Step 1,1", "Step 2,3", "Step 4,5", "Step 6,7", "Exit")]
        public void TestCommandsCalled(params string[] commands)
        {
            int commandsInterpreted = 0;
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns(() => commands[commandsInterpreted])
                .Callback(() => commandsInterpreted += 1);

            _program.Execute(new CommandInterpreter(_program));

            Assert.AreEqual(commands.Length, commandsInterpreted);
        }

        [TestCase("Exit")]
        [TestCase("Step 1,1", "Exit")]
        [TestCase("Step 1,1", "Step 2,3", "Step 4,5", "Step 6,7", "Exit")]
        public void StateGetsDisplayedBeforeStartAndAfterEachCommand(params string[] commands)
        {
            int commandsInterpreted = 0;
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns(() => commands[commandsInterpreted])
                .Callback(() => commandsInterpreted += 1);

            int stateDisplayCount = 0;
            _communicatorMock.Setup(communicator => communicator.DisplayState(It.IsAny<string>()))
                .Callback(() => stateDisplayCount += 1);

            _program.Execute(new CommandInterpreter(_program));

            Assert.AreEqual(commands.Length, stateDisplayCount);
        }

        [TestCase("statePrintResult1")]
        [TestCase("statePrintResult2")]
        public void DisplayingStatusPassesInStatusFromGame(string statePrintResult)
        {
            var gameStateMock = new Mock<IGameState>();
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns("Exit");
            _gameMock.Setup(game => game.State)
                .Returns(gameStateMock.Object);
            _gameStatePrinterMock.Setup(
                printer => printer.Print(
                    It.Is<IGameState>(
                        state => ReferenceEquals(state, gameStateMock.Object))))
                .Returns(statePrintResult);
            string displayedState = null;
            _communicatorMock.Setup(communicator => communicator.DisplayState(It.IsAny<string>()))
                .Callback((string message) => displayedState = message);

            _program.Execute(new CommandInterpreter(_program));

            Assert.AreEqual(statePrintResult, displayedState);
        }
    }
}
