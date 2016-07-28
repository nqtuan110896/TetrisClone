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
        public const int TotalColumn = 4;
        public const int TotalRow = 4;
        
        public static uint FallTime = 500u;

        protected Point2D _basePosition;
        protected int[][] _cells;
        protected int _cellWidth;
        protected int _currentColumn;
        protected int _currentRow;
        protected uint _fallDelay;
        #endregion

        #region Constructors
        public Tetromino(int startRow, int startCol, Point2D basePosition, int cellWidth)
        {
            _basePosition = basePosition;
            _cellWidth = cellWidth;
            _cells = new int[TotalRow][];
            for (int row = 0; row < TotalRow; ++row)
            {
                _cells[row] = new int[TotalColumn];
                for (int col = 0; col < TotalColumn; ++col) _cells[row][col] = 0;
            }
            _currentColumn = startCol;
            _currentRow = startRow;
            _fallDelay = 0u;
        }
        #endregion

        #region Properties
        public static Color Background
        {
            get; set;
        }

        public static Color Foreground
        {
            get; set;
        }

        public Point2D BasePosition
        {
            get { return _basePosition; }
            set { _basePosition = value; }
        }

        public int CellWidth
        {
            get { return _cellWidth; }
            set { _cellWidth = value; }
        }
        #endregion

        #region Methods
        public bool CanMoveDown(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row][col] != 0)
                    {
                        if (_currentRow + row + 1 >= Board.TotalRow) return false;
                        else if (board[_currentRow + row + 1][_currentColumn + col] != 0) return false;
                    }
                }
            }
            return true;
        }

        public bool CanMoveLeft(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row][col] != 0)
                    {
                        if (_currentColumn + col - 1 < 0) return false;
                        else if (board[_currentRow + row][_currentColumn + col - 1] != 0) return false;
                    }
                }
            }
            return true;
        }

        public bool CanMoveRight(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row][col] != 0)
                    {
                        if (_currentColumn + col + 1 >= Board.TotalColumn) return false;
                        else if (board[_currentRow + row][_currentColumn + col + 1] != 0) return false;
                    }
                }
            }
            return true;
        }

        public bool CanMoveUp(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row][col] != 0)
                    {
                        if (_currentRow + row - 1 < 0) return false;
                        else if (board[_currentRow + row - 1][_currentColumn + col] != 0) return false;
                    }
                }
            }
            return true;
        }

        public void Drop()
        {
            Fall(32.0f);
        }

        public void Fall(float speed = 1.0f)
        {
            _fallDelay += Convert.ToUInt32(speed * GameMain.DeltaTime);
            if (_fallDelay >= FallTime)
            {
                MoveDown();
                _fallDelay = 0u;
            }
        }

        public void Land(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row][col] != 0) board[row + _currentRow][col + _currentColumn] = _cells[row][col];
                }
            }
        }

        public void MoveDown()
        {
            _currentRow++;
        }

        public void MoveLeft()
        {
            _currentColumn--;
        }

        public void MoveRight()
        {
            _currentColumn++;
        }

        public void MoveUp()
        {
            _currentRow--;
        }

        public void Render()
        {
            Vector offset;
            Point2D renderPosition;

            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    offset = SwinGame.VectorTo(Convert.ToSingle((_currentColumn + col) * _cellWidth), Convert.ToSingle((_currentRow + row) * _cellWidth));
                    renderPosition = _basePosition.Add(offset);
                    if (_cells[row][col] != 0)
                    {
                        SwinGame.FillRectangle(Background, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                        SwinGame.DrawRectangle(Foreground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                    }
                }
            }
        }

        public void Rotate(Board board, string direction = "Clockwise")
        {
            while (!TryRotate(board, direction))
            {
                // Try performing necessary wall kicks
                if (CanMoveRight(board)) MoveRight();
                else if (CanMoveLeft(board)) MoveLeft();
            }
        }

        public bool TryRotate(Board board, string direction)
        {
            if (_currentColumn < 0 || _currentColumn + TotalColumn > Board.TotalColumn) return false;
            else if (_currentRow + TotalRow > Board.TotalRow) return false;

            int[][] tmp = new int[TotalRow][];
            for (int row = 0; row < TotalRow; ++row)
            {
                tmp[row] = new int[TotalColumn];
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (direction.ToLower() == "counterclockwise") tmp[row][col] = _cells[col][_cells.GetUpperBound(0) - row];
                    else tmp[row][col] = _cells[_cells.GetUpperBound(0) - col][row]; // Default rotation direction is Clockwise

                    if (board[_currentRow + row][_currentColumn + col] != 0 && tmp[row][col] != 0) return false;
                }
            }
            _cells = tmp;
            return true;
        }

        public void RotateClockwise(Board board)
        {
            Rotate(board);
        }

        public void RotateCounterClockwise(Board board)
        {
            Rotate(board, "CounterClockwise");
        }
        #endregion
    }
}
