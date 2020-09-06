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
        }

        [Test]
        public void ExecutionStopsAfterCallingExit()
        {
            var interpreterMock = new Mock<ICommandInterpreter>();
            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, interpreterMock.Object);
            interpreterMock.Setup(mock => mock.Interpret(It.IsAny<string>(), It.IsAny<IProgram>()))
                .Callback(() => program.Exit());
            program.Execute();

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

            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, new CommandInterpreter());
            program.Execute();

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
            
            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, new CommandInterpreter());

            program.Execute();

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

            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, new CommandInterpreter());

            program.Execute();

            Assert.AreEqual(statePrintResult, displayedState);
        }

        [Test]
        public void Win_CommunicatesWin()
        {
            _gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Returns(GameStatus.Win);

            var commandQueue = new Queue<string>(new[] { "Step 1,1", "exit" });
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns(() => commandQueue.Dequeue());

            _communicatorMock.Setup(communicator => communicator.AskForNewGame())
                .Returns("No");

            int communicateWinCalls = 0;
            _communicatorMock.Setup(communicator => communicator.CommunicateWin())
                .Callback(() => communicateWinCalls += 1);

            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, new CommandInterpreter());
            program.Execute();

            Assert.AreEqual(1, communicateWinCalls);
        }

        [Test]
        public void Loss_CommunicatesLoss()
        {
            _gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Returns(GameStatus.Loss);

            var commandQueue = new Queue<string>(new[] { "Step 1,1", "exit" });
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns(() => commandQueue.Dequeue());

            _communicatorMock.Setup(communicator => communicator.AskForNewGame())
                .Returns("No");

            int communicateLossCalls = 0;
            _communicatorMock.Setup(communicator => communicator.CommunicateLoss())
                .Callback(() => communicateLossCalls += 1);

            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, new CommandInterpreter());
            program.Execute();

            Assert.AreEqual(1, communicateLossCalls);
        }

        [TestCase(GameStatus.Loss, 2, 1, "No", "Step 1,1")]
        [TestCase(GameStatus.Win, 2, 1, "No", "Step 1,1")]
        [TestCase(GameStatus.Loss, 3, 2, "Yes", "Step 1,1", "Exit")]
        [TestCase(GameStatus.Win, 3, 2, "Yes", "Step 1,1", "Exit")]
        public void WinOrLoss_ShowsStateAndAskForAnotherGame_ClosesAfterNo(GameStatus statusAfterGameCommand, int expectedStateDisplayCounter, int expectedGameCreationCounter, string startNewGame, params string[] commands)
        {
            _gameMock.Setup(game => game.ExecuteCommand(It.IsAny<IGameCommand>()))
                .Returns(statusAfterGameCommand);

            var commandQueue = new Queue<string>(commands);
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns(() => commandQueue.Dequeue());

            _communicatorMock.Setup(communicator => communicator.AskForNewGame())
                .Returns(startNewGame);

            int stateDisplayCounter = 0;
            _communicatorMock.Setup(communicator => communicator.DisplayState(It.IsAny<string>()))
                .Callback(() => stateDisplayCounter += 1);

            int gameCreationCounter = 0;
            _gameFactoryMock.Setup(factory => factory.Create())
                .Callback(() => gameCreationCounter += 1)
                .Returns(_gameMock.Object);

            var program = new Program(_communicatorMock.Object, _gameFactoryMock.Object, _gameStatePrinterMock.Object, new CommandInterpreter());
            program.Execute();

            Assert.AreEqual(expectedStateDisplayCounter, stateDisplayCounter);
            Assert.AreEqual(expectedGameCreationCounter, gameCreationCounter);
        }

        [TestCase("Yes")]
        [TestCase("No")]
        public void AskForNewGame_AsksCommunicatorAndPassesResponseToInterpreter(string response)
        {
            _communicatorMock.Setup(communicator => communicator.AskForNewGame())
                .Returns(response);

            var interpreterMock = new Mock<ICommandInterpreter>();
            var program = new Program(
                _communicatorMock.Object, 
                _gameFactoryMock.Object, 
                _gameStatePrinterMock.Object, 
                interpreterMock.Object);

            int interpretCallCount = 0;
            interpreterMock.Setup(interpreter => interpreter.InterpretNewGameYesNo(It.Is<string>(command => command == response), It.Is<IProgram>(prog => prog == program)))
                .Callback(() => interpretCallCount += 1);

            program.AskForNewGame();

            Assert.AreEqual(1, interpretCallCount);
        }

        [TestCase("Step 1,1")]
        [TestCase("Flag 5,5")]
        public void AskForCommand_AsksCommunicatorAndPassesResponseToInterpreter(string response)
        {
            _communicatorMock.Setup(communicator => communicator.AskForCommand())
                .Returns(response);

            var interpreterMock = new Mock<ICommandInterpreter>();
            var program = new Program(
                _communicatorMock.Object,
                _gameFactoryMock.Object,
                _gameStatePrinterMock.Object,
                interpreterMock.Object);

            int interpretCallCount = 0;
            interpreterMock.Setup(interpreter => interpreter.Interpret(It.Is<string>(command => command == response), It.Is<IProgram>(prog => prog == program)))
                .Callback(() => interpretCallCount += 1);

            program.AskForCommand();

            Assert.AreEqual(1, interpretCallCount);
        }
    }
}
