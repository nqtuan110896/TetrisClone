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
        public static void HandleInput()
        {
            SwinGame.ProcessEvents();
            if (SwinGame.KeyTyped(KeyCode.vk_SPACE))
            {
                float oldScale = GameConfig.GameScale;
                GameConfig.GameScale = 1.5f / oldScale;
                float scaleRatio = GameConfig.GameScale / oldScale;
                
                GameConfig.ScreenHeight = Convert.ToInt32(GameConfig.ScreenHeight * scaleRatio);
                GameConfig.ScreenWidth = Convert.ToInt32(GameConfig.ScreenWidth * scaleRatio);
                SwinGame.ChangeScreenSize(GameConfig.ScreenWidth, GameConfig.ScreenHeight);

                _tetromino.CellWidth = Convert.ToInt32(_tetromino.CellWidth * scaleRatio);
            }
            if (SwinGame.KeyTyped(KeyCode.vk_e)) _tetromino.RotateClockwise();
            else if (SwinGame.KeyTyped(KeyCode.vk_q)) _tetromino.RotateCounterClockwise();
        }

        public static void InitialiseComponents()
        {
            SwinGame.OpenGraphicsWindow(GameConfig.GameNameWithVersion(), GameConfig.ScreenWidth, GameConfig.ScreenHeight);
            _timer = SwinGame.CreateTimer();
            _tetromino = new TetrominoI(0, 3, 40);
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