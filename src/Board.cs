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
        public const int TotalRow = 22;

        private Color _background;
        private Point2D _basePosition;
        private int[,] _cells;
        private int _cellWidth;
        private Color _foreground;
        #endregion

        #region Constructors
        public Board(Point2D basePosition, int cellWidth)
        {
            _background = SwinGame.ColorTransparent();
            _basePosition = basePosition;

            _cells = new int[TotalRow, TotalColumn];
            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col) _cells[row, col] = 0;
            }

            _cellWidth = cellWidth;
            _foreground = GameConfig.Background;
        }
        #endregion

        #region Properties
        public Color Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public int CellWidth
        {
            get { return _cellWidth; }
            set { _cellWidth = value; }
        }

        public Color Foreground
        {
            get { return _foreground; }
            set { _foreground = value; }
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
        public void Render()
        {
            Color cellBackground;
            Vector offset;
            Point2D renderPosition;

            for (int row = 0; row < TotalRow; ++row)
            {
                for (int col = 0; col < TotalColumn; ++col)
                {
                    switch (_cells[row, col])
                    {
                        case 1: // TetrominoI background is cyan #00FFFF.
                            cellBackground = SwinGame.RGBColor(0, 255, 255);
                            break;
                        case 2: // TetrominoJ background is blue #0000FF.
                            cellBackground = SwinGame.RGBColor(0, 0, 255);
                            break;
                        case 3: // TetrominoL background is orange #FF9900.
                            cellBackground = SwinGame.RGBColor(255, 153, 0);
                            break;
                        case 4: // TetrominoO background is yellow #FFFF00.
                            cellBackground = SwinGame.RGBColor(255, 255, 0);
                            break;
                        case 5: // TetrominoS background is lime #80FF00.
                            cellBackground = SwinGame.RGBColor(128, 255, 0);
                            break;
                        case 6: // TetrominoT background is purple #800080.
                            cellBackground = SwinGame.RGBColor(128, 0, 128);
                            break;
                        case 7: // TetrominoZ background is red #FF0000.
                            cellBackground = SwinGame.RGBColor(255, 0, 0);
                            break;
                        default: // case 0
                            cellBackground = _background;
                            break;
                    }
                    offset = SwinGame.VectorTo(Convert.ToSingle(col * _cellWidth), Convert.ToSingle(row * _cellWidth));
                    renderPosition = _basePosition.Add(offset);
                    SwinGame.FillRectangle(cellBackground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                    SwinGame.DrawRectangle(_foreground, SwinGame.RectangleFrom(renderPosition, _cellWidth, _cellWidth));
                }
            }
        }
        #endregion
    }
}
