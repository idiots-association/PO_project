using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PO_game.Src.States
{
    public class StateManager
    {
        private Stack<State> states;
        public StateManager()
        {
            states = new Stack<State>();
        }

        public void AddState(State state)
        {
            state.LoadContent();
            states.Push(state);
        }
        
        public void RemoveState()
        {
            states.Pop();
        }
        public State getCurrentState()
        {
            return states.Peek();
        }

    }
}