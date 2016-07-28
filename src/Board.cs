using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace TetrisClone
{
    public class Board
    {
        #region Fields
        public const int TotalColumn = 10;
        public const int TotalRow = 20;

        private Point2D _basePosition;
        private int[][] _cells;
        private int _cellWidth;
        #endregion

        #region Constructors
        public Board(Point2D basePosition, int cellWidth)
        {
            Foreground = GameConfig.Background;
            _basePosition = basePosition;
            _cellWidth = cellWidth;
            _cells = new int[TotalRow][];
            for (int row = 0; row < TotalRow; ++row)
            {
                _cells[row] = new int[TotalColumn];
                for (int col = 0; col < TotalColumn; ++col) _cells[row][col] = 0;
            }
        }
        #endregion

        #region Properties
        public static Color Foreground
        {
            get; set;
        }
        #endregion

        #region Indexers
        public int[] this[int row]
        {
            get { return _cells[row]; }
            set { _cells[row] = value; }
        }

        public int this[int row, int col]
        {
            get { return _cells[row][col]; }
            set { _cells[row][col] = value; }
        }
        #endregion

        #region Methods
        public void Render()
        {
            Color cellBackground;
            Vector offset;
            Point2D renderPosition;

            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    switch (_cells[row][col])
                    {
                        case 0:
                            cellBackground = GameConfig.Background;
                            break;
                        default:
                            cellBackground = TetrominoI.Background;
                            break;
                    }
                    offset = SwinGame.VectorTo(Convert.ToSingle(col * _cellWidth), Convert.ToSingle(row * _cellWidth));
                    renderPosition = _basePosition.Add(offset);
                    SwinGame.FillRectangle(cellBackground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                    SwinGame.DrawRectangle(Foreground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                }
            }
        }
        #endregion
    }
}
