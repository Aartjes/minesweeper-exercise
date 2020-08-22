using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public interface IFieldProbe
    {
        FieldProbeResult Probe(Field field, int x, int y);
    }
}
