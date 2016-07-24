using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace TetrisClone
{
    public abstract class Tetromino
    {
        #region Fields
        protected Color _background;
        protected int[][] _cells;
        protected int _cellWidth;
        protected int _column;
        protected Color _foreground;
        protected int _row;
        #endregion

        #region Constructors
        public Tetromino(int startRow, int startCol, int cellWidth)
        {
            _cellWidth = cellWidth;
            _cells = new int[4][];
            for (int row = 0; row < _cells.Length; ++row)
            {
                _cells[row] = new int[4];
                for (int col = 0; col < _cells[row].Length; ++col) _cells[row][col] = 0;
            }
            _column = startCol;
            _row = startRow;
        }
        #endregion

        #region Properties

        #endregion

        #region Methods
        public void Render()
        {
            Vector offset;
            Point2D renderPosition, startPosition = SwinGame.PointAt(0, 0);

            for (int row = 0; row < _cells.Length; ++row)
            {
                for (int col = 0; col < _cells[row].Length; ++col)
                {
                    offset = SwinGame.VectorTo(Convert.ToSingle((_column + col) * _cellWidth), Convert.ToSingle((_row + row) * _cellWidth));
                    renderPosition = startPosition.Add(offset);
                    if (_cells[row][col] != 0)
                    {
                        SwinGame.FillRectangle(_background, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                        SwinGame.DrawRectangle(_foreground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                    }
                }
            }
        }

        public void Rotate(string direction = "Clockwise")
        {
            int[][] tmp = new int[_cells.Length][];

            for (int row = 0; row < _cells.Length; ++row)
            {
                tmp[row] = new int[_cells[row].Length];
                for (int col = 0; col < _cells.Length; ++col)
                {
                    if (direction.ToLower() == "counterclockwise") tmp[row][col] = _cells[col][_cells.GetUpperBound(0) - row];
                    else tmp[row][col] = _cells[_cells.GetUpperBound(0) - col][row]; // Default rotation direction is Clockwise
                }
            }

            _cells = tmp;
        }

        public void RotateClockwise()
        {
            Rotate();
        }

        public void RotateCounterClockwise()
        {
            Rotate("CounterClockwise");
        }
        #endregion
    }
}
