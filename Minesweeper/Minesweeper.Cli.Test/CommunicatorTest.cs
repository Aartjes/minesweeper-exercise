using Com.Github.Aartjes.Minesweeper.Cli;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitlab.Aartjes.Minesweeper.Cli.Test
{
    public class CommunicatorTest
    {
        private Mock<ITextResources> _resourcesMock;
        private Mock<IConsole> _consoleMock;
        private Communicator _communicator;

        [SetUp]
        public void SetUp()
        {
            _resourcesMock = new Mock<ITextResources>();
            _consoleMock = new Mock<IConsole>();
            _communicator = new Communicator(_resourcesMock.Object, _consoleMock.Object);
        }

        [TestCase("Step 2 0")]
        [TestCase("Exit")]
        public void AskForCommand_ReturnsWhatIsReadByConsole(string command)
        {
            _consoleMock.Setup(console => console.ReadLine())
                .Returns(command);

            var result = _communicator.AskForCommand();

            Assert.AreEqual(command, result);
        }

        [TestCase("Step 2 0")]
        [TestCase("Exit")]
        public void AskForCommand_WritesCommandQueryFromResources(string commandQuery)
        {
            _resourcesMock.Setup(resources => resources.CommandQuery)
                .Returns(commandQuery);

            int amountOfCommandQueryWrites = 0;
            _consoleMock.Setup(console => console.WriteLine(It.Is<string>(line => line == commandQuery)))
                .Callback(() => amountOfCommandQueryWrites += 1);

            _communicator.AskForCommand();

            Assert.AreEqual(1, amountOfCommandQueryWrites);
        }

        [TestCase("Yes")]
        [TestCase("No")]
        public void AskForNewGame_ReturnsWhatIsReadByConsole(string command)
        {
            _consoleMock.Setup(console => console.ReadLine())
                .Returns(command);

            var result = _communicator.AskForNewGame();

            Assert.AreEqual(command, result);
        }

        [TestCase("Yes")]
        [TestCase("No")]
        public void AskForCommand_WritesNewGameQueryFromResources(string newGameQuery)
        {
            _resourcesMock.Setup(resources => resources.NewGameQuery)
                .Returns(newGameQuery);

            int amountOfNewGameQueryWrites = 0;
            _consoleMock.Setup(console => console.WriteLine(It.Is<string>(line => line == newGameQuery)))
                .Callback(() => amountOfNewGameQueryWrites += 1);

            _communicator.AskForNewGame();

            Assert.AreEqual(1, amountOfNewGameQueryWrites);
        }

        [TestCase("You lost")]
        [TestCase("You stepped on a mine.")]
        public void CommunicateLoss_WritesLossMessageFromResources(string lossMessage)
        {
            _resourcesMock.Setup(resources => resources.LossMessage)
                .Returns(lossMessage);

            int amountOfLossMessagesWritten = 0;
            _consoleMock.Setup(console => console.WriteLine(It.Is<string>(line => line == lossMessage)))
                .Callback(() => amountOfLossMessagesWritten += 1);

            _communicator.CommunicateLoss();

            Assert.AreEqual(1, amountOfLossMessagesWritten);
        }

        [TestCase("You Won!")]
        [TestCase("You have successfully cleared all non-mine fields!")]
        public void CommunicateWin_WritesVictoryMessageFromResources(string victoryMessage)
        {
            _resourcesMock.Setup(resources => resources.VictoryMessage)
                .Returns(victoryMessage);

            int amountOfVictoryeMessagesWritten = 0;
            _consoleMock.Setup(console => console.WriteLine(It.Is<string>(line => line == victoryMessage)))
                .Callback(() => amountOfVictoryeMessagesWritten += 1);

            _communicator.CommunicateWin();

            Assert.AreEqual(1, amountOfVictoryeMessagesWritten);
        }

        [TestCase("imagine a gamestate here")]
        [TestCase("imagine some other gamestate here")]
        public void DisplayState_WritesStateToConsole(string printedState)
        {

            int amountOfStateWrites = 0;
            _consoleMock.Setup(console => console.WriteLine(It.Is<string>(line => line == printedState)))
                .Callback(() => amountOfStateWrites += 1);

            _communicator.DisplayState(printedState);

            Assert.AreEqual(1, amountOfStateWrites);
        }
    }
}
