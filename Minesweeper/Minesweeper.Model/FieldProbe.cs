using System;

namespace Com.Github.Aartjes.Minesweeper.Model
{
    public class FieldProbe
    {
        public FieldProbeResult Probe(bool[,] field, int x, int y)
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

        private int SpaceExistsAndHasMine(bool[,] field, int x, int y)
        {
            return Convert.ToInt32(
                x >= 0 
                && x<field.GetLength(0)
                && y >= 0  
                && y < field.GetLength(1) 
                && field[x, y]);
        }
    }
}
