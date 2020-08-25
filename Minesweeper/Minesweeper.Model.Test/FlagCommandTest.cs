using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class FlagCommandTest
    {
        [Test]
        public void Execute_RunsToggleFlagOnFieldAtGivenSpace()
        {
            bool testSuccesful = false;
            var gameStateMock = new Mock<IGameState>();
            gameStateMock.Setup(
                state => state.ToggleFlag(
                    It.Is<int>(x => x == 12),
                    It.Is<int>(y => y == 13)))
                .Callback(() => testSuccesful = true);
            new FlagCommand(12, 13)
                .Execute(gameStateMock.Object);


            Assert.IsTrue(testSuccesful);
        }
    }
}
