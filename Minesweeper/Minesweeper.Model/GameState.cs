using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class GameState
    {
        private Field _field;
        private GameSpaceState _state = GameSpaceState.Blank;

        public GameState(Field field)
        {
            _field = field;
        }

        public GameSpaceState this[int x, int y] => _state;

        public int FieldWidth => _field.Width;
        public int FieldHeight => _field.Height;

        public void ToggleFlag(int x, int y)
        {
            if (_state != GameSpaceState.Flag)
            {
                _state = GameSpaceState.Flag;
            }
            else
            {
                _state = GameSpaceState.Blank;
            }
        }
    }
}
