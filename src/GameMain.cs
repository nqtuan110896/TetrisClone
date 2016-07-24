using System;
using SwinGameSDK;

namespace TetrisClone
{
    public class GameMain
    {
        #region Fields
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
        public static void InitialiseComponents()
        {
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            _timer = SwinGame.CreateTimer();
            _tetromino = new TetrominoI(0, 3, 40);
        }

        public static void HandleInput()
        {
            SwinGame.ProcessEvents();
            if (SwinGame.KeyTyped(KeyCode.vk_e)) _tetromino.RotateClockwise();
            else if (SwinGame.KeyTyped(KeyCode.vk_q)) _tetromino.RotateCounterClockwise();
        }

        public static void Main()
        {
            InitialiseComponents();
            Run();
            Terminate();
        }

        public static void Render()
        {
            SwinGame.ClearScreen(Color.White);
            _tetromino.Render();
            SwinGame.RefreshScreen(60);
        }

        public static void Run()
        {
            do
            {
                HandleInput();
                Update();
                Render();
            } while (!SwinGame.WindowCloseRequested());
        }

        public static void Terminate()
        {
            SwinGame.ReleaseAllResources();
        }

        public static void Update()
        {
            LastFrameTime = _timer.Ticks;
        }
        #endregion
    }
}