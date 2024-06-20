using System.Collections.Generic;


namespace PO_game.Src.Screens
{
    /// <summary>
    /// <c>ScreenManager</c> class to manage screens.
    /// <para>
    /// It contains instance of itself to ensure so that there is only one Screen Manager per build.</para>
    /// </summary>
    public class ScreenManager
    {
        private static ScreenManager _instance;
        private Stack<Screen> _screens;

        private ScreenManager()
        {
            _screens = new Stack<Screen>();
        }

        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScreenManager();
                }
                return _instance;
            }
        }

        public void AddScreen(Screen Screen)
        {
            Screen.LoadContent();
            _screens.Push(Screen);
        }

        public void RemoveScreen()
        {
            _screens.Pop();
        }
        public Screen GetCurrentScreen()
        {
            return _screens.Peek();
        }
    }

}