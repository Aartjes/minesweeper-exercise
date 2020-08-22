using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class GameState
    {
        private Field _field;

        public GameState(Field field)
        {
            _field = field;
        }

        public GameSpaceState this[int x, int y] => GameSpaceState.Blank;

        public int FieldWidth => _field.Width;
        public int FieldHeight => _field.Height;
    }
}
