using NUnit.Framework;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class GameTest
    {
        private bool[,] _fieldArray;
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _fieldArray = new bool[15, 15];
            _game = new Game(
                new GameState(
                    new Field(_fieldArray),
                    new FieldProbe(),
                    new SpaceStateConvertor())
                    );
        }

        [Test]
        public void ExecuteCommand_StepOnMine_ResultIsLoss()
        {
            _fieldArray[12, 13] = true;

            var command = new StepCommand(12, 13);
            var status = _game.ExecuteCommand(command);

            Assert.AreEqual(GameStatus.Loss, status);
        }

        [Test]
        public void ExecuteCommand_StepNotOnMine_NotOnlyMinesLeft_ResultIsOngoing()
        {
            _fieldArray[12, 13] = true;

            var command = new StepCommand(12, 12);
            var status = _game.ExecuteCommand(command);

            Assert.AreEqual(GameStatus.Ongoing, status);

        }

        [Test]
        public void ExecuteCommand_StepNotOnMine_OnlyMinesLeft_ResultIsWin()
        {
            _fieldArray[12, 12] = true;

            var command = new StepCommand(0, 0);
            var status = _game.ExecuteCommand(command);

            Assert.AreEqual(GameStatus.Win, status);
        }

        [Test]
        public void ExecuteCommand_StepNotOnMine_OnlyFlaggedMinesLeft_ResultIsWin()
        {
            _fieldArray[12, 12] = true;

            var status = _game.ExecuteCommand(new FlagCommand(12, 12));
            Assert.AreEqual(GameStatus.Ongoing, status);

            status = _game.ExecuteCommand(new StepCommand(0, 0));
            Assert.AreEqual(GameStatus.Win, status);
        }
    }
}
