using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class GameState
    {
        private Field _field;
        private readonly IFieldProbe _probe;
        private readonly ISpaceStateConvertor _spaceStateConverter;
        private GameSpaceState _state = GameSpaceState.Blank;

        public GameState(Field field, IFieldProbe probe, ISpaceStateConvertor spaceStateConverter)
        {
            _field = field;
            _probe = probe;
            _spaceStateConverter = spaceStateConverter;
        }

        public GameSpaceState this[int x, int y] => _state;

        public int FieldWidth => _field.Width;
        public int FieldHeight => _field.Height;

        public void ToggleFlag(int x, int y)
        {
            switch(_state)
            {
                case GameSpaceState.Blank:
                    _state = GameSpaceState.Flag;
                    break;
                case GameSpaceState.Flag:
                    _state = GameSpaceState.Blank;
                    break;
            }
        }

        public void Step(int x, int y)
        {
            _state = _spaceStateConverter.Convert(_probe.Probe(_field, x, y));
        }
    }
}
