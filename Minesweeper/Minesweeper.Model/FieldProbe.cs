using System;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FieldProbe : IFieldProbe
    {
        public FieldProbeResult Probe(Field field, int x, int y)
        {
            int neighborMineCount = 0;
            if (field[x, y])
            {
                return FieldProbeResult.Mine;
            }
            neighborMineCount += SpaceExistsAndHasMine(field, x - 1, y);
            neighborMineCount += SpaceExistsAndHasMine(field, x, y - 1);
            neighborMineCount += SpaceExistsAndHasMine(field, x - 1, y - 1);
            neighborMineCount += SpaceExistsAndHasMine(field, x + 1, y);
            neighborMineCount += SpaceExistsAndHasMine(field, x, y + 1);
            neighborMineCount += SpaceExistsAndHasMine(field, x + 1, y + 1);
            neighborMineCount += SpaceExistsAndHasMine(field, x + 1, y - 1);
            neighborMineCount += SpaceExistsAndHasMine(field, x - 1, y + 1);
            return (FieldProbeResult)neighborMineCount;
        }

        private int SpaceExistsAndHasMine(Field field, int x, int y)
        {
            return Convert.ToInt32(
                x >= 0 
                && x<field.Width
                && y >= 0  
                && y < field.Height 
                && field[x, y]);
        }
    }
}
