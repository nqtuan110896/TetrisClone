using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace TetrisClone
{
    public class TetrominoT : Tetromino
    {
        #region Constructors
        public TetrominoT(int startRow, int startCol, Point2D basePosition, int cellWidth) : base(startRow, startCol, basePosition, cellWidth)
        {
            _background = SwinGame.RGBColor(128, 0, 128);
            _cells = new int[TotalRow, TotalColumn]
            {
                {0, 6, 0, 0},
                {6, 6, 6, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0},
            };
        }
        #endregion

        #region Methods
        public override bool TryRotateClockwise(Board board)
        {
            int[,] tmp;
            switch (_rotationStage)
            {
                case 1:
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 0, 0, 0},
                        {6, 6, 6, 0},
                        {0, 6, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                case 2:
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 6, 0, 0},
                        {6, 6, 0, 0},
                        {0, 6, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                case 3:
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 6, 0, 0},
                        {6, 6, 6, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                default: // case 0
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 6, 0, 0},
                        {0, 6, 6, 0},
                        {0, 6, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
            }
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (tmp[row, col] != 0)
                    {
                        if (_currentColumn + col < 0 || _currentColumn + col >= Board.TotalColumn || _currentRow + row >= Board.TotalRow) return false;
                        else if (tmp[row, col] != 0 && board[_currentRow + row, _currentColumn + col] != 0) return false;
                    }
                }
            }
            _cells = tmp;
            return true;
        }

        public override bool TryRotateCounterClockwise(Board board)
        {
            int[,] tmp;
            switch (_rotationStage)
            {
                case 1:
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 6, 0, 0},
                        {6, 6, 6, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                case 2:
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 6, 0, 0},
                        {0, 6, 6, 0},
                        {0, 6, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                case 3:
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 0, 0, 0},
                        {6, 6, 6, 0},
                        {0, 6, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
                default: // case 0
                    tmp = new int[TotalRow, TotalColumn]
                    {
                        {0, 6, 0, 0},
                        {6, 6, 0, 0},
                        {0, 6, 0, 0},
                        {0, 0, 0, 0}
                    };
                    break;
            }
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (tmp[row, col] != 0)
                    {
                        if (_currentColumn + col < 0 || _currentColumn + col >= Board.TotalColumn || _currentRow + row >= Board.TotalRow) return false;
                        else if (tmp[row, col] != 0 && board[_currentRow + row, _currentColumn + col] != 0) return false;
                    }
                }
            }
            _cells = tmp;
            return true;
        }
        #endregion
    }
}
