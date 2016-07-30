using System;
using SwinGameSDK;

namespace TetrisClone
{
    public class GameMain
    {
        #region Fields
        private static Board _board;
        private static uint _currentLockDelay;
        private static Tetromino _tetromino;
        private static Timer _timer;
        #endregion

        #region Properties
        public static uint DeltaTime
        {
            get { return _timer.Ticks - LastFrameTime; }
        }

        public static uint ElapsedTime
        {
            get { return _timer.Ticks; }
        }

        public static uint LastFrameTime
        {
            get; set;
        }

        public static uint MaxLockDelay
        {
            get; private set;
        }
        #endregion

        #region Methods
        public static Tetromino CreateRandomTetromino()
        {
            Random rng = new Random();
            return CreateTetromino(rng.Next(7));
        }

        public static Tetromino CreateTetromino(int tetrominoType)
        {
            int rowOnBoard = 0;
            int columnOnBoard = 3;
            int cellWidth = 36;
            Point2D basePosition = SwinGame.PointAt(GameConfig.ScreenWidth / 2.0f - 5.0f * cellWidth, -2.0f * cellWidth);
            switch (tetrominoType)
            {
                case 1: return new TetrominoJ(rowOnBoard, columnOnBoard, basePosition, cellWidth);
                case 2: return new TetrominoL(rowOnBoard, columnOnBoard, basePosition, cellWidth);
                case 3: return new TetrominoO(rowOnBoard, columnOnBoard, basePosition, cellWidth);
                case 4: return new TetrominoS(rowOnBoard, columnOnBoard, basePosition, cellWidth);
                case 5: return new TetrominoT(rowOnBoard, columnOnBoard, basePosition, cellWidth);
                case 6: return new TetrominoZ(rowOnBoard, columnOnBoard, basePosition, cellWidth);
                default: return new TetrominoI(rowOnBoard, columnOnBoard, basePosition, cellWidth); // case 0
            }
        }

        public static void HandleInput()
        {
            SwinGame.ProcessEvents();

            if (SwinGame.KeyTyped(KeyCode.vk_f)) SwinGame.ToggleFullScreen(); ;
            if (SwinGame.KeyTyped(KeyCode.vk_e)) _tetromino.RotateClockwise(_board);
            else if (SwinGame.KeyTyped(KeyCode.vk_q)) _tetromino.RotateCounterClockwise(_board);
            if (SwinGame.KeyTyped(KeyCode.vk_e)) Console.WriteLine(_tetromino.RotationStage);
            else if (SwinGame.KeyTyped(KeyCode.vk_q)) Console.WriteLine(_tetromino.RotationStage);

            if (SwinGame.KeyTyped(KeyCode.vk_UP))
            {
                while (_tetromino.CanMoveDown(_board)) _tetromino.Fall();
                _currentLockDelay = MaxLockDelay / 2;
            }
            else if (SwinGame.KeyTyped(KeyCode.vk_DOWN) && _tetromino.CanMoveDown(_board)) _tetromino.Drop();
            else if (SwinGame.KeyDown(KeyCode.vk_SPACE) && _tetromino.CanMoveUp(_board)) _tetromino.MoveUp();

            if (SwinGame.KeyTyped(KeyCode.vk_LEFT) && _tetromino.CanMoveLeft(_board)) _tetromino.MoveLeft();
            else if (SwinGame.KeyTyped(KeyCode.vk_RIGHT) && _tetromino.CanMoveRight(_board)) _tetromino.MoveRight();
        }

        public static void InitialiseComponents()
        {
            SwinGame.OpenGraphicsWindow(GameConfig.GameNameWithVersion(), GameConfig.ScreenWidth, GameConfig.ScreenHeight);
            // cellWidth, currently set to 36
            _board = new Board(SwinGame.PointAt(GameConfig.ScreenWidth / 2.0f - 5.0f * 36, -2.0f * 36), 36);
            _tetromino = CreateRandomTetromino();

            MaxLockDelay = 1000u;
            _currentLockDelay = 0u;
            _timer = SwinGame.CreateTimer();
        }

        public static void Main()
        {
            InitialiseComponents();
            Run();
            Terminate();
        }

        public static void Render()
        {
            SwinGame.ClearScreen(GameConfig.Background);
            _board.Render();
            _tetromino.Render();
            SwinGame.RefreshScreen(60);
        }

        public static void Run()
        {
            _timer.Start();
            do
            {
                HandleInput();
                Update();
                Render();
            } while (!SwinGame.WindowCloseRequested());
        }

        public static void Terminate()
        {
            _timer.Stop();
            SwinGame.ReleaseAllResources();
        }

        public static void Update()
        {
            if (_tetromino.CanMoveDown(_board))
            {
                _currentLockDelay = 0u;
                _tetromino.Fall();
            }
            else
            {
                _currentLockDelay += DeltaTime;
                if (_currentLockDelay >= MaxLockDelay)
                {
                    _tetromino.Land(_board);
                    _tetromino = CreateRandomTetromino();
                    _currentLockDelay = 0u;
                }
            }
            _tetromino.Update();
            LastFrameTime = _timer.Ticks;
        }
        #endregion
    }
}