using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using System.Collections.Generic;

namespace PO_game.Src
{
    public class Player : Character
    {
        private Vector2 _destination;
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int maxMana { get; set; }
        public int mana { get; set; }
        public Weapon weapon { get; set; }
        public Player(Sprite sprite) : base(sprite)
        {
            _destination = Sprite.Position;
        }

        private void RemoveOldPositionFromCollisionMap(Dictionary<Vector2, int> collisionMap)
        {
            Vector2 oldPlayerTile = new Vector2(
                (int)(Sprite.Position.X / GlobalSettings.TileSize),
                (int)((Sprite.Position.Y + Sprite.Position.Y % GlobalSettings.TileSize) / GlobalSettings.TileSize)
            );
            if (collisionMap.ContainsKey(oldPlayerTile) && collisionMap[oldPlayerTile] == (int)Collision.PlayerCollision)
            {
                collisionMap.Remove(oldPlayerTile);
            }
        }

        private void AddPlayerPositionToCollisionMap(Vector2 playerTile, Dictionary<Vector2, int> collisionMap)
        {
            collisionMap[playerTile] = (collisionMap.ContainsKey(playerTile) && collisionMap[playerTile] > (int)Collision.NoColission) ? collisionMap[playerTile] : (int)Collision.PlayerCollision;
        }


        public void MovePlayer(GameTime gameTime, InputController inputController, Dictionary<Vector2, int> collisionMap)
        {
            switch(State)
            {
                case CharacterState.Idle:
                    Vector2 playerTile;
                    if (inputController.IsKeyDown(Keys.W) || inputController.IsKeyDown(Keys.Up))
                    { 
                        playerTile = new Vector2(
                            (int)(Sprite.Position.X / GlobalSettings.TileSize), 
                            (int)((Sprite.Position.Y + Sprite.Position.Y % GlobalSettings.TileSize) / GlobalSettings.TileSize) - 1
                            );
                        if (!(collisionMap.ContainsKey(playerTile)) || collisionMap[playerTile] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingUp;
                            _destination.Y -= GlobalSettings.TileSize;
                            AddPlayerPositionToCollisionMap(playerTile, collisionMap);
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.S) || inputController.IsKeyDown(Keys.Down))
                    {
                        playerTile = new Vector2(
                            (int)(Sprite.Position.X / GlobalSettings.TileSize), 
                            (int)((Sprite.Position.Y + Sprite.Position.Y % GlobalSettings.TileSize) / GlobalSettings.TileSize) + 1
                            );
                        if (!(collisionMap.ContainsKey(playerTile)) || collisionMap[playerTile] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingDown;
                            _destination.Y += GlobalSettings.TileSize;
                            AddPlayerPositionToCollisionMap(playerTile, collisionMap);
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.A) || inputController.IsKeyDown(Keys.Left))
                    {
                        playerTile = new Vector2(
                            (int)(Sprite.Position.X / GlobalSettings.TileSize) - 1, 
                            (int)((Sprite.Position.Y + Sprite.Position.Y%GlobalSettings.TileSize) / GlobalSettings.TileSize)
                            );
                        if (!(collisionMap.ContainsKey(playerTile)) || collisionMap[playerTile] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingLeft;
                            _destination.X -= GlobalSettings.TileSize;
                            AddPlayerPositionToCollisionMap(playerTile, collisionMap);
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.D) || inputController.IsKeyDown(Keys.Right))
                    {
                        playerTile = new Vector2(
                            (int)(Sprite.Position.X / GlobalSettings.TileSize) + 1, 
                            (int)((Sprite.Position.Y + Sprite.Position.Y % GlobalSettings.TileSize) / GlobalSettings.TileSize)
                            );
                        if (!(collisionMap.ContainsKey(playerTile)) || collisionMap[playerTile] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingRight;
                            _destination.X += GlobalSettings.TileSize;
                            AddPlayerPositionToCollisionMap(playerTile, collisionMap);
                        }
                    }
                    break;

                case CharacterState.MovingUp:
                    if(Sprite.Position.Y - (GlobalSettings.MoveSpeed * GlobalSettings.Scale * gameTime.ElapsedGameTime.TotalMilliseconds) < _destination.Y)
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
                    if(Sprite.Position.Y + (GlobalSettings.MoveSpeed * GlobalSettings.Scale *  gameTime.ElapsedGameTime.TotalMilliseconds) > _destination.Y)
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
                    if(Sprite.Position.X - (GlobalSettings.MoveSpeed * GlobalSettings.Scale * gameTime.ElapsedGameTime.TotalMilliseconds) < _destination.X)
                    {
                        Sprite.Position = new Vector2(_destination.X, Sprite.Position.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X - (int)(GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds), Sprite.Position.Y);
                    }
                    break;

                case CharacterState.MovingRight:
                    if(Sprite.Position.X + (GlobalSettings.MoveSpeed * GlobalSettings.Scale * gameTime.ElapsedGameTime.TotalMilliseconds) > _destination.X)
                    {
                        Sprite.Position = new Vector2(_destination.X, Sprite.Position.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X + (int)(GlobalSettings.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds), Sprite.Position.Y);
                    }
                    break;

            }

        }



        public void Update(GameTime gameTime, InputController inputController, Dictionary<Vector2, int> collisionMap)
        {
            MovePlayer(gameTime, inputController, collisionMap);
            //base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(Sprite.Position.X - Sprite.Texture.Width / 2f, Sprite.Position.Y - Sprite.Texture.Height / 2f + Sprite.Texture.Height / 2f);
            spriteBatch.Draw(Sprite.Texture, position, Color.White);
        }


    }
}
