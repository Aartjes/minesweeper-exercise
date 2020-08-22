using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class SpaceStateConvertor : ISpaceStateConvertor
    {
        public GameSpaceState Convert(FieldProbeResult spaceState)
        {
            return (GameSpaceState)spaceState;
        }
    }
}
