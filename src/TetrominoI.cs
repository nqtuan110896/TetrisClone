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
        public TetrominoI(int startRow, int startCol, int cellWidth) : base(startRow, startCol, cellWidth)
        {
            _background = SwinGame.RGBColor(0, 255, 255);
            _foreground = GameConfig.Background;
            for (int row = 0; row < _cells.Length; ++row) _cells[row][1] = 1;
        }
        #endregion
    }
}
