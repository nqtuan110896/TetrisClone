using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace TetrisClone
{
    public static class GameConfig
    {
        #region Fields
        public const string GameName = "TetrisClone";
        public const string GameVersion = "0.1.1";

        private static Lazy<Color> _background;
        private static Lazy<Color> _backgroundInactive;
        private static Lazy<Color> _foreground;
        private static Lazy<Color> _foregroundInactive;
        #endregion

        #region Constructors
        static GameConfig()
        {
            _background = new Lazy<Color>(() => { return SwinGame.RGBColor(240, 240, 240); });
            _backgroundInactive = new Lazy<Color>(() => { return SwinGame.RGBAColor(Background.R, Background.G, Background.B, 64); });
            _foreground = new Lazy<Color>(() => { return SwinGame.RGBColor(16, 16, 16); });
            _foregroundInactive = new Lazy<Color>(() => { return SwinGame.RGBAColor(Foreground.R, Foreground.G, Foreground.B, 64); });

            GameScale = 1.0f;
            ScreenHeight = 720;
            ScreenWidth = 1280;
        }
        #endregion

        #region Properties
        public static Color Background
        {
            get { return _background.Value; }
            set { _background = new Lazy<Color>(() => { return value; }); }
        }

        public static Color BackgroundInactive
        {
            get { return _backgroundInactive.Value; }
            set { _backgroundInactive = new Lazy<Color>(() => { return value; }); }
        }

        public static Color Foreground
        {
            get { return _foreground.Value; }
            set { _foreground = new Lazy<Color>(() => { return value; }); }
        }

        public static Color ForegroundInactive
        {
            get { return _foregroundInactive.Value; }
            set { _foregroundInactive = new Lazy<Color>(() => { return value; }); }
        }

        public static float GameScale
        {
            get; set;
        }

        public static int ScreenHeight
        {
            get; set;
        }

        public static int ScreenWidth
        {
            get; set;
        }
        #endregion

        #region Methods
        public static string GameNameWithVersion()
        {
            return string.Format("{0} v{1}", GameName, GameVersion);
        }
        #endregion
    }
}
