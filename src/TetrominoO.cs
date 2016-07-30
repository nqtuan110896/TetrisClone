using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace TetrisClone
{
    public class TetrominoO : Tetromino
    {
        #region Constructors
        public TetrominoO(int startRow, int startCol, Point2D basePosition, int cellWidth) : base(startRow, startCol, basePosition, cellWidth)
        {
            _background = SwinGame.RGBColor(255, 255, 0);

            _cells = new int[TotalRow, TotalColumn]
            {
                {0, 4, 4, 0},
                {0, 4, 4, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0}
            };
        }
        #endregion

        #region Methods
        public override bool TryRotateClockwise(Board board)
        {
            return true;
        }

        public override bool TryRotateCounterClockwise(Board board)
        {
            return true;
        }
        #endregion
    }
}
