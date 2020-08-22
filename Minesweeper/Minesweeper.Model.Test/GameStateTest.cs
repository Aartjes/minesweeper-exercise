using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model.Test
{
    [TestFixture]
    public class GameStateTest
    {
        private Field _field;

        [SetUp]
        public void SetUp()
        {
            var fieldArray = new bool[10, 10];
            fieldArray[0, 0] = true;
            fieldArray[1, 1] = true;
            fieldArray[2, 2] = true;
            fieldArray[4, 2] = true;
            fieldArray[3, 3] = true;
            fieldArray[5, 3] = true;
            fieldArray[6, 3] = true;
            fieldArray[4, 4] = true;
            fieldArray[6, 4] = true;
            fieldArray[7, 4] = true;
            fieldArray[5, 5] = true;
            fieldArray[7, 5] = true;
            fieldArray[8, 5] = true;
            fieldArray[5, 6] = true;
            fieldArray[6, 6] = true;
            fieldArray[8, 6] = true;
            fieldArray[6, 7] = true;
            fieldArray[7, 7] = true;
            fieldArray[8, 7] = true;
            fieldArray[9, 7] = true;
            fieldArray[7, 8] = true;
            fieldArray[9, 8] = true;
            fieldArray[7, 9] = true;
            fieldArray[8, 9] = true;
            fieldArray[9, 9] = true;
            _field = new Field(fieldArray);
        }

        [Test]
        public void GameStateStartsWithAllSpacesBlank()
        {
            var gameState = new GameState(_field);
            Assert.AreEqual(10, gameState.FieldWidth);
            Assert.AreEqual(10, gameState.FieldHeight);

            for (int x = 0; x < gameState.FieldWidth; x++)
            {
                for (int y = 0; y < gameState.FieldHeight; y++)
                {
                    if (gameState[x, y] != GameSpaceState.Blank)
                    {
                        Assert.Fail();
                    }
                }
            }
            Assert.Pass();
        }

        [Test]
        public void ToggleFlag_TogglesFlagOnAndOff()
        {
            var gameState = new GameState(_field);
            Assert.AreEqual(GameSpaceState.Blank, gameState[0, 0]);
            gameState.ToggleFlag(0, 0);
            Assert.AreEqual(GameSpaceState.Flag, gameState[0, 0]);
            gameState.ToggleFlag(0, 0);
            Assert.AreEqual(GameSpaceState.Blank, gameState[0, 0]);
        }
    }
}
