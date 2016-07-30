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
        
        public static uint MaxFallDelay = 500u;

        protected Color _background;
        protected Point2D _basePosition;
        protected int[,] _cells;
        protected int _cellWidth;
        protected int _currentColumn;
        protected uint _currentFallDelay;
        protected int _currentRow;
        protected Color _foreground;
        protected int _rotationStage;
        #endregion

        #region Constructors
        protected Tetromino(int startRow, int startCol, Point2D basePosition, int cellWidth)
        {
            _basePosition = basePosition;
            _cellWidth = cellWidth;
            _currentColumn = startCol;
            _currentFallDelay = 0u;
            _currentRow = startRow;
            _foreground = GameConfig.Background;
            _rotationStage = 0;
        }
        #endregion

        #region Properties
        public Color Background
        {
            get { return _background; }
            set { _background = value; }
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

        public int CurrentColumn
        {
            get { return _currentColumn; }
            set { _currentColumn = value; }
        }

        public uint CurrentFallDelay
        {
            get { return _currentFallDelay; }
            set { _currentFallDelay = value; }
        }

        public int CurrentRow
        {
            get { return _currentRow; }
            set { _currentRow = value; }
        }

        public Color Foreground
        {
            get { return _foreground; }
            set { _foreground = value; }
        }

        public int RotationStage
        {
            get { return _rotationStage; }
            set { _rotationStage = value; }
        }
        #endregion

        #region Indexers
        public int this[int row, int col]
        {
            get { return _cells[row, col]; }
            set { _cells[row, col] = value; }
        }
        #endregion

        #region Methods
        public bool CanMoveDown(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row, col] != 0)
                    {
                        if (_currentRow + row + 1 >= Board.TotalRow) return false;
                        else if (board[_currentRow + row + 1, _currentColumn + col] != 0) return false;
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
                    if (_cells[row, col] != 0)
                    {
                        if (_currentColumn + col - 1 < 0) return false;
                        else if (board[_currentRow + row, _currentColumn + col - 1] != 0) return false;
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
                    if (_cells[row, col] != 0)
                    {
                        if (_currentColumn + col + 1 >= Board.TotalColumn) return false;
                        else if (board[_currentRow + row, _currentColumn + col + 1] != 0) return false;
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
                    if (_cells[row, col] != 0)
                    {
                        if (_currentRow + row - 1 < 0) return false;
                        else if (board[_currentRow + row - 1, _currentColumn + col] != 0) return false;
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
            _currentFallDelay += Convert.ToUInt32(speed * GameMain.DeltaTime);
            if (_currentFallDelay >= MaxFallDelay)
            {
                MoveDown();
                _currentFallDelay = 0u;
            }
        }

        public void Land(Board board)
        {
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    if (_cells[row, col] != 0) board[row + _currentRow, col + _currentColumn] = _cells[row, col];
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
                    if (_cells[row, col] != 0)
                    {
                        SwinGame.FillRectangle(_background, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                    }
                    SwinGame.DrawRectangle(_foreground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                }
            }
        }

        public void RotateClockwise(Board board)
        {
            if (TryRotateClockwise(board))
            {
                _rotationStage++;
                return;
            }

            if (CanMoveRight(board))
            {
                MoveRight();
                if (TryRotateClockwise(board))
                {
                    _rotationStage++;
                    return;
                }

                if (CanMoveRight(board))
                {
                    MoveRight();
                    if (TryRotateClockwise(board))
                    {
                        _rotationStage++;
                        return;
                    }
                    MoveLeft();
                }
                MoveLeft();
            }

            if (CanMoveLeft(board))
            {
                MoveLeft();
                if (TryRotateClockwise(board))
                {
                    _rotationStage++;
                    return;
                }

                if (CanMoveLeft(board))
                {
                    MoveLeft();
                    if (TryRotateClockwise(board))
                    {
                        _rotationStage++;
                        return;
                    }
                    MoveRight();
                }
                MoveRight();
            }
        }

        public void RotateCounterClockwise(Board board)
        {
            if (TryRotateCounterClockwise(board))
            {
                _rotationStage--;
                return;
            }

            if (CanMoveLeft(board))
            {
                MoveLeft();
                if (TryRotateCounterClockwise(board))
                {
                    _rotationStage--;
                    return;
                }

                if (CanMoveLeft(board))
                {
                    MoveLeft();
                    if (TryRotateCounterClockwise(board))
                    {
                        _rotationStage--;
                        return;
                    }
                    MoveRight();
                }
                MoveRight();
            }

            if (CanMoveRight(board))
            {
                MoveRight();
                if (TryRotateCounterClockwise(board))
                {
                    _rotationStage--;
                    return;
                }

                if (CanMoveRight(board))
                {
                    MoveRight();
                    if (TryRotateCounterClockwise(board))
                    {
                        _rotationStage--;
                        return;
                    }
                    MoveLeft();
                }
                MoveLeft();
            }
        }

        public abstract bool TryRotateClockwise(Board board);
        public abstract bool TryRotateCounterClockwise(Board board);

        public void Update()
        {
            if (_rotationStage < 0) _rotationStage += 4;
            else if (_rotationStage > 3) _rotationStage -= 4;
        }
        #endregion
    }
}
