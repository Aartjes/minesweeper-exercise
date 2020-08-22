using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class StepCommandTest
    {
        [Test]
        public void StepCommandPerformsStepOnSelectedSpace()
        {
            bool calledStep = false;
            var gameStateMock = new Mock<IGameState>();
            gameStateMock.Setup(
                state => state.Step(
                    It.Is<int>(x => x == 7),
                    It.Is<int>(y => y == 6)))
                .Callback(() => calledStep = true);

            gameStateMock.Setup(state => state[It.IsAny<int>(), It.IsAny<int>()])
                .Returns(() => calledStep ? GameSpaceState.One : GameSpaceState.Blank);

            gameStateMock
                .Setup(state=>state.FieldWidth)
                .Returns(10);
            gameStateMock
                .Setup(state => state.FieldHeight)
                .Returns(10);

            new StepCommand(7, 6, gameStateMock.Object).Execute();

            Assert.IsTrue(calledStep);
        }

        [Test]
        public void SteppingOnHigherThan0OnlyStepsThere()
        {
            var fieldArray = new bool[2, 2];
            fieldArray[1, 1] = true;
            var gameState = new GameState(new Field(fieldArray), new FieldProbe(), new SpaceStateConvertor());
            var command = new StepCommand(0, 0, gameState);

            command.Execute();

            Assert.AreEqual(GameSpaceState.One, gameState[0, 0]);
            Assert.AreEqual(GameSpaceState.Blank, gameState[0, 1]);
            Assert.AreEqual(GameSpaceState.Blank, gameState[1, 0]);
            Assert.AreEqual(GameSpaceState.Blank, gameState[1, 1]);
        }

        [Test]
        public void Stepping0SpreadsOutUntilNot0()
        {
            var fieldArray = new bool[7, 5];
            fieldArray[0, 1] = true;
            fieldArray[6, 1] = true;
            fieldArray[6, 3] = true;
            fieldArray[2, 4] = true;
            var state = new GameState(new Field(fieldArray), new FieldProbe(), new SpaceStateConvertor());

            new StepCommand(3, 1, state)
                .Execute();

            Assert.AreEqual(GameSpaceState.Blank, state[0, 0]);
            Assert.AreEqual(GameSpaceState.One, state[1, 0]);
            Assert.AreEqual(GameSpaceState.Zero, state[2, 0]);
            Assert.AreEqual(GameSpaceState.Zero, state[3, 0]);
            Assert.AreEqual(GameSpaceState.Zero, state[4, 0]);
            Assert.AreEqual(GameSpaceState.One, state[5, 0]);
            Assert.AreEqual(GameSpaceState.Blank, state[6, 0]);

            Assert.AreEqual(GameSpaceState.Blank, state[0, 1]);
            Assert.AreEqual(GameSpaceState.One, state[1, 1]);
            Assert.AreEqual(GameSpaceState.Zero, state[2, 1]);
            Assert.AreEqual(GameSpaceState.Zero, state[3, 1]);
            Assert.AreEqual(GameSpaceState.Zero, state[4, 1]);
            Assert.AreEqual(GameSpaceState.One, state[5, 1]);
            Assert.AreEqual(GameSpaceState.Blank, state[6, 1]);

            Assert.AreEqual(GameSpaceState.Blank, state[0, 2]);
            Assert.AreEqual(GameSpaceState.One, state[1, 2]);
            Assert.AreEqual(GameSpaceState.Zero, state[2, 2]);
            Assert.AreEqual(GameSpaceState.Zero, state[3, 2]);
            Assert.AreEqual(GameSpaceState.Zero, state[4, 2]);
            Assert.AreEqual(GameSpaceState.Two, state[5, 2]);
            Assert.AreEqual(GameSpaceState.Blank, state[6, 2]);

            Assert.AreEqual(GameSpaceState.Blank, state[0, 3]);
            Assert.AreEqual(GameSpaceState.One, state[1, 3]);
            Assert.AreEqual(GameSpaceState.One, state[2, 3]);
            Assert.AreEqual(GameSpaceState.One, state[3, 3]);
            Assert.AreEqual(GameSpaceState.Zero, state[4, 3]);
            Assert.AreEqual(GameSpaceState.One, state[5, 3]);
            Assert.AreEqual(GameSpaceState.Blank, state[6, 3]);

            Assert.AreEqual(GameSpaceState.Blank, state[0, 4]);
            Assert.AreEqual(GameSpaceState.Blank, state[1, 4]);
            Assert.AreEqual(GameSpaceState.Blank, state[2, 4]);
            Assert.AreEqual(GameSpaceState.One, state[3, 4]);
            Assert.AreEqual(GameSpaceState.Zero, state[4, 4]);
            Assert.AreEqual(GameSpaceState.One, state[5, 4]);
            Assert.AreEqual(GameSpaceState.Blank, state[6, 4]);
        }
    }
}
