using System.Linq;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class Field
    {
        private bool[,] _fieldArray;

        public Field(bool[,] fieldArray)
        {
            _fieldArray = fieldArray;
        }

        public int Width => _fieldArray.GetLength(0);
        public int Height => _fieldArray.GetLength(1);
        public int MineCount => _fieldArray.Cast<bool>().Count(value => value);

        public bool this[int x, int y]
        {
            get
            {
                return _fieldArray[x, y];
            }
        }
    }
}
