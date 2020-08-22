using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface ISpaceStateConverter
    {
        GameSpaceState Convert(FieldProbeResult spaceState);
    }
}
