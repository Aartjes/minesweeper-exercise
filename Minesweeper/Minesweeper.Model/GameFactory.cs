using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class GameFactory : IGameFactory
    {
        private readonly IFieldProbe _fieldProbe;
        private readonly ISpaceStateConvertor _spaceStateConvertor;
        private Random _random;

        public GameFactory(IFieldProbe fieldProbe, ISpaceStateConvertor spaceStateConvertor) 
            : this(fieldProbe, spaceStateConvertor, new Random())
        { }

        public GameFactory(IFieldProbe fieldProbe, ISpaceStateConvertor spaceStateConvertor, Random random)
        {
            _fieldProbe = fieldProbe;
            _spaceStateConvertor = spaceStateConvertor;
            _random = random;
        }

        public int FieldWidth { get; set; } = 9;
        public int FieldHeight { get; set; } = 9;
        public int MineCount { get; set; } = 10;

        public IGame Create()
        {
            var fieldArray = GenerateFieldArray();

           return new Game(
               new GameState(
                   new Field(fieldArray), 
                   _fieldProbe, 
                   _spaceStateConvertor));
        }

        private bool[,] GenerateFieldArray()
        {
            List<(int X, int Y)> availablePoints = new List<(int, int)>();
            for (int y = 0; y < FieldHeight; y++)
            {
                for (int x = 0; x < FieldWidth; x++)
                {
                    (int X, int Y) point = (x, y);
                    availablePoints.Add(point);
                }
            }

            var fieldArray = new bool[FieldWidth, FieldHeight];
            for(int mineCounter = 0; mineCounter < MineCount; mineCounter++)
            {
                int randomIndex = _random.Next(0, availablePoints.Count);
                var randomPoint = availablePoints[randomIndex];
                availablePoints.RemoveAt(randomIndex);
                fieldArray[randomPoint.X, randomPoint.Y] = true;
            }
            return fieldArray;
        }
    }
}
