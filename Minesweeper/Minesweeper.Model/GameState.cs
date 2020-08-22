using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class GameState
    {
        private Field _field;
        private readonly IFieldProbe _probe;
        private readonly ISpaceStateConverter _spaceStateConverter;
        private GameSpaceState[,] _state;

        public GameState(Field field, IFieldProbe probe, ISpaceStateConverter spaceStateConverter)
        {
            _field = field;
            _probe = probe;
            _spaceStateConverter = spaceStateConverter;
            _state = new GameSpaceState[FieldWidth, FieldHeight];
            for (int x = 0; x < FieldWidth; x++)
            {
                for (int y = 0; y < FieldHeight; y++)
                {
                    _state[x, y] = GameSpaceState.Blank;
                }
            }
        }

        public GameSpaceState this[int x, int y] => _state[x, y];

        public int FieldWidth => _field.Width;
        public int FieldHeight => _field.Height;

        public void ToggleFlag(int x, int y)
        {
            switch (_state[x, y])
            {
                case GameSpaceState.Blank:
                    _state[x, y] = GameSpaceState.Flag;
                    break;
                case GameSpaceState.Flag:
                    _state[x, y] = GameSpaceState.Blank;
                    break;
            }
        }

        public void Step(int x, int y)
        {
            _state[x, y] = _spaceStateConverter.Convert(_probe.Probe(_field, x, y));
        }
    }
}
