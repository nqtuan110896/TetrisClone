using System;
using SwinGameSDK;

namespace TetrisClone
{
    public class GameMain
    {
        #region Fields
        private static Board _board;
        private static uint _lockDelay;
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
        #endregion

        #region Methods
        public static void HandleInput()
        {
            SwinGame.ProcessEvents();

            if (SwinGame.KeyTyped(KeyCode.vk_f)) SwinGame.ToggleFullScreen(); ;
            if (SwinGame.KeyTyped(KeyCode.vk_e)) _tetromino.RotateClockwise(_board);
            else if (SwinGame.KeyTyped(KeyCode.vk_q)) _tetromino.RotateCounterClockwise(_board);

            if (SwinGame.KeyTyped(KeyCode.vk_UP))
            {
                while (_tetromino.CanMoveDown(_board)) _tetromino.Fall();
                _lockDelay = Tetromino.FallTime * 2;
            }
            else if (SwinGame.KeyDown(KeyCode.vk_DOWN) && _tetromino.CanMoveDown(_board)) _tetromino.Drop();
            else if (SwinGame.KeyDown(KeyCode.vk_SPACE) && _tetromino.CanMoveUp(_board)) _tetromino.MoveUp();

            if (SwinGame.KeyTyped(KeyCode.vk_LEFT) && _tetromino.CanMoveLeft(_board)) _tetromino.MoveLeft();
            else if (SwinGame.KeyTyped(KeyCode.vk_RIGHT) && _tetromino.CanMoveRight(_board)) _tetromino.MoveRight();
        }

        public static void InitialiseComponents()
        {
            SwinGame.OpenGraphicsWindow(GameConfig.GameNameWithVersion(), GameConfig.ScreenWidth, GameConfig.ScreenHeight);
            _lockDelay = 0u;
            _timer = SwinGame.CreateTimer();
            // cellWidth, currently set to 45
            _board = new Board(SwinGame.PointAt(GameConfig.ScreenWidth / 2.0f - 5.0f * 45, -4.0f * 45), 45);
            _tetromino = new TetrominoI(0, 3, SwinGame.PointAt(GameConfig.ScreenWidth / 2.0f - 5.0f * 45, -4.0f * 45), 45);
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
                _lockDelay = 0u;
                _tetromino.Fall();
            }
            else
            {
                _lockDelay += DeltaTime;
                if (_lockDelay >= Tetromino.FallTime * 2)
                {
                    _tetromino.Land(_board);
                    _tetromino = new TetrominoI(0, 3, SwinGame.PointAt(GameConfig.ScreenWidth / 2.0f - 5.0f * 45, -4.0f * 45), 45);
                    _lockDelay = 0u;
                }
            }
            LastFrameTime = _timer.Ticks;
        }
        #endregion
    }
}