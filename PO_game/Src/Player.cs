using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Utils;

namespace PO_game.Src
{
    public class Player : Character
    {
        private Vector2 _destination;
        public Player(Sprite sprite) : base(sprite)
        {
            _destination = Sprite.Position;
        }

        public void MovePlayer(GameTime gameTime, InputController inputController)
        {
            switch(State)
            {
                case CharacterState.Idle:
                    if(inputController.IsKeyDown(Keys.W) || inputController.IsKeyDown(Keys.Up))
                    {
                        State = CharacterState.MovingUp;
                        _destination.Y -= GlobalSettings.TileSize;
                        
                    }
                    else if(inputController.IsKeyDown(Keys.S) || inputController.IsKeyDown(Keys.Down))
                    {
                        State = CharacterState.MovingDown;
                        _destination.Y += GlobalSettings.TileSize;
                    }
                    else if(inputController.IsKeyDown(Keys.A) || inputController.IsKeyDown(Keys.Left))
                    {
                        State = CharacterState.MovingLeft;
                        _destination.X -= GlobalSettings.TileSize;
                    }
                    else if(inputController.IsKeyDown(Keys.D) || inputController.IsKeyDown(Keys.Right))
                    {
                        State = CharacterState.MovingRight;
                        _destination.X += GlobalSettings.TileSize;
                    }
                    break;

                 case CharacterState.MovingUp:
                    if(Sprite.Position.Y - (GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds) < _destination.Y)
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, _destination.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y - (int)(GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds));
                    }
                    break;

                case CharacterState.MovingDown:
                    if(Sprite.Position.Y + (GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds) > _destination.Y)
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, _destination.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y + (int)(GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds));
                    }
                    break;
                
                case CharacterState.MovingLeft:
                    if(Sprite.Position.X - (GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds) < _destination.X)
                    {
                        Sprite.Position = new Vector2(_destination.X, Sprite.Position.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Position = new Vector2(Sprite.Position.X - (int)(GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds), Sprite.Position.Y);
                    }
                    break;

                case CharacterState.MovingRight:
                    if(Sprite.Position.X + (GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds) > _destination.X)
                    {
                        Sprite.Position = new Vector2(_destination.X, Sprite.Position.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X + (int)(GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds), Position.Y);
                    }
                    break;

            }

        }

        public void Update(GameTime gameTime, InputController inputController)
        {
            MovePlayer(gameTime, inputController);
            //base.Update(gameTime);
        }

    }
}
