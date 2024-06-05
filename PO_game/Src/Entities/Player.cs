using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Inv;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using System;
using System.Collections.Generic;

namespace PO_game.Src.Entities
{
    public class Player : Character
    {
        private Vector2 _destination;
        public Weapon weapon { get; set; }
        public Inventory inventory { get; set; }
        public Player(Sprite sprite, Vector2 tilePosition, Inventory inventory) : base(sprite, tilePosition)
        {
            this.inventory = inventory;
            maxHealth = 100;
            maxMana = 100;
            health = maxHealth;  //should not be like that
            mana = maxMana;     //need to change it after a proper fighting implementation is done
            _destination = Sprite.Position;
        }

        private void RemoveOldPositionFromCollisionMap(Dictionary<Vector2, int> collisionMap)
        {
            Vector2 oldPlayerTile = new Vector2(
                (int)(Sprite.Position.X / Globals.TileSize),
                (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
            );
            if (collisionMap.ContainsKey(oldPlayerTile) && collisionMap[oldPlayerTile] == (int)Collision.PlayerCollision)
            {
                collisionMap.Remove(oldPlayerTile);
            }
        }

        private void AddPlayerPositionToCollisionMap(Vector2 playerTile, Dictionary<Vector2, int> collisionMap)
        {
            collisionMap[TilePosition] = collisionMap.ContainsKey(TilePosition) && collisionMap[TilePosition] > (int)Collision.NoColission ? collisionMap[TilePosition] : (int)Collision.PlayerCollision;
        }


        public void MovePlayer(GameTime gameTime, InputController inputController, Dictionary<Vector2, int> collisionMap)
        {
            TilePosition = new Vector2(
                (int)(Sprite.Position.X / Globals.TileSize),
                (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
            );
            switch (State)
            {
                case CharacterState.Idle:
                    if (inputController.IsKeyDown(Keys.W) || inputController.IsKeyDown(Keys.Up))
                    {
                        TilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize),
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize) - 1
                            );
                        if (!collisionMap.ContainsKey(TilePosition) || collisionMap[TilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingUp;
                            _destination.Y -= Globals.TileSize;
                            AddPlayerPositionToCollisionMap(TilePosition, collisionMap);
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.S) || inputController.IsKeyDown(Keys.Down))
                    {
                        TilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize),
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize) + 1
                            );
                        if (!collisionMap.ContainsKey(TilePosition) || collisionMap[TilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingDown;
                            _destination.Y += Globals.TileSize;
                            AddPlayerPositionToCollisionMap(TilePosition, collisionMap);
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.A) || inputController.IsKeyDown(Keys.Left))
                    {
                        TilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize) - 1,
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
                            );
                        if (!collisionMap.ContainsKey(TilePosition) || collisionMap[TilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingLeft;
                            _destination.X -= Globals.TileSize;
                            AddPlayerPositionToCollisionMap(TilePosition, collisionMap);
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.D) || inputController.IsKeyDown(Keys.Right))
                    {
                        TilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize) + 1,
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
                            );
                        if (!collisionMap.ContainsKey(TilePosition) || collisionMap[TilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingRight;
                            _destination.X += Globals.TileSize;
                            AddPlayerPositionToCollisionMap(TilePosition, collisionMap);
                        }
                    }
                    break;

                case CharacterState.MovingUp:
                    if (Sprite.Position.Y - Globals.MoveSpeed * Globals.Scale * gameTime.ElapsedGameTime.TotalMilliseconds < _destination.Y)
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, _destination.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y - (int)(Globals.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds));
                    }
                    break;

                case CharacterState.MovingDown:
                    if (Sprite.Position.Y + Globals.MoveSpeed * Globals.Scale * gameTime.ElapsedGameTime.TotalMilliseconds > _destination.Y)
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, _destination.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y + (int)(Globals.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds));
                    }
                    break;

                case CharacterState.MovingLeft:
                    if (Sprite.Position.X - Globals.MoveSpeed * Globals.Scale * gameTime.ElapsedGameTime.TotalMilliseconds < _destination.X)
                    {
                        Sprite.Position = new Vector2(_destination.X, Sprite.Position.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X - (int)(Globals.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds), Sprite.Position.Y);
                    }
                    break;

                case CharacterState.MovingRight:
                    if (Sprite.Position.X + Globals.MoveSpeed * Globals.Scale * gameTime.ElapsedGameTime.TotalMilliseconds > _destination.X)
                    {
                        Sprite.Position = new Vector2(_destination.X, Sprite.Position.Y);
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Sprite.Position = new Vector2(Sprite.Position.X + (int)(Globals.MoveSpeed * gameTime.ElapsedGameTime.TotalMilliseconds), Sprite.Position.Y);
                    }
                    break;

            }

        }
        public void Attack(Enemy enemy)
        {
            if (weapon != null)
            {
                weapon.Attack(enemy);
            }
            else
            {
                Random random = new Random();
                enemy.health -= random.Next(2, 4);
            }
        }

        public void Update(GameTime gameTime, InputController inputController, Dictionary<Vector2, int> collisionMap)
        {
            MovePlayer(gameTime, inputController, collisionMap);
            if (inputController.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                inventory.showInventory = !inventory.showInventory;

            }
            // if(inputController.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.P))//temporary
            // {
            //     inventory.AddItem(_medpot);
            // }
            // if(inputController.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.M))//temporary
            // {
            //     inventory.AddItem(_mace);
            // }
            // if(inputController.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.F))//temporary
            // {
            //     inventory.AddItem(_dagger);
            // }
            if (inventory.showInventory)
            {
                inventory.Update();
            }
            //base.Update(gameTime);
        }

        public void UpdatePosition(Vector2 tilePosition)
        {
            TilePosition = tilePosition;
            Sprite.Position = new Vector2(
                TilePosition.X * Globals.TileSize + Globals.TileSize / 2 + Sprite.Texture.Width % Globals.TileSize,
                TilePosition.Y * Globals.TileSize - Sprite.Texture.Height % Globals.TileSize);
            _destination = Sprite.Position;
        }
    }
}
