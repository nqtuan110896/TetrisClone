using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace TetrisClone
{
    public class TetrominoI : Tetromino
    {
        #region Constructors
        public TetrominoI(int startRow, int startCol, Point2D basePosition, int cellWidth) : base(startRow, startCol, basePosition, cellWidth)
        {
            Background = SwinGame.RGBColor(0, 255, 255);
            Foreground = GameConfig.Background;
            for (int col = 0; col < TotalColumn; ++col) _cells[1][col] = 1;
        }
        #endregion
    }
}
