using Moq;
using NUnit.Framework;
using System;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    public class GameFactoryTest
    {
        private Mock<IFieldProbe> _fieldProbeMock;
        private Mock<ISpaceStateConvertor> _spaceStateConverterMock;
        private GameFactory _gameFactory;

        [SetUp]
        public void SetUp()
        {
            _fieldProbeMock = new Mock<IFieldProbe>();
            _spaceStateConverterMock = new Mock<ISpaceStateConvertor>();
            _gameFactory = new GameFactory(_fieldProbeMock.Object,_spaceStateConverterMock.Object);
        }

        [Test]
        public void GameFactory_StartsWith9x9With10Mines()
        {
            Assert.AreEqual(9, _gameFactory.FieldWidth);
            Assert.AreEqual(9, _gameFactory.FieldHeight);
            Assert.AreEqual(10, _gameFactory.MineCount);
        }

        [TestCase(26, 24, 100)]
        [TestCase(20, 30, 55)]
        public void Create_ParametersArePassedOnToCreatedGame(int fieldWidth, int fieldHeight, int mineCount)
        {
            _gameFactory.FieldWidth = fieldWidth;
            _gameFactory.FieldHeight = fieldHeight;
            _gameFactory.MineCount = mineCount;

            var game = _gameFactory.Create();

            Assert.AreEqual(game.FieldWidth, _gameFactory.FieldWidth);
            Assert.AreEqual(game.FieldHeight, _gameFactory.FieldHeight);
            Assert.AreEqual(game.MineCount, _gameFactory.MineCount);
        }

        [TestCase(0, 2, 7)]
        [TestCase(1, 4, 2)]
        [TestCase(2, 7, 7)]
        [TestCase(3, 9, 2)]
        [TestCase(4, 1, 8)]
        public void TestRandomMineLocation_BasedOnHardcodedSeeds(int randomSeed, int mineX, int mineY)
        {
            _gameFactory = new GameFactory(
                new FieldProbe(), 
                new SpaceStateConvertor(), 
                new Random(randomSeed));
            _gameFactory.FieldWidth = 10;
            _gameFactory.FieldHeight = 10;
            _gameFactory.MineCount = 1;

            var game = _gameFactory.Create();

            game.State.Step(mineX, mineY);
            Assert.AreEqual(GameSpaceState.Mine, game.State[mineX, mineY]);
        }
    }
}
