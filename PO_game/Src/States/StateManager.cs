using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PO_game.Src.States
{
    public class StateManager
    {
        private static StateManager _instance;
        private Stack<State> _states;

        private StateManager()
        {
            _states = new Stack<State>();
        }

        public static StateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateManager();
                }
                return _instance;
            }
        }

        public void AddState(State state)
        {
            state.LoadContent();
            _states.Push(state);
        }

        public void RemoveState()
        {
            _states.Pop();
        }
        public State GetCurrentState()
        {
            return _states.Peek();
        }
    }

}