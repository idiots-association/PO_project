using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Inv;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using PO_game.Src.Controls;
using Microsoft.Xna.Framework.Content;

namespace PO_game.Src.Entities
{
    /// <summary>
    /// <c>Player</c> is a class that represents the player character in the game.
    /// <para>The player class stores all the player's attributes, such as health, mana, weapon and inventory and some movement and collision logic.</para>
    /// </summary>
    public class Player : Character
    {
        private Vector2 _destination;
        public Weapon weapon { get; set; }
        public Inventory inventory { get; set; }
           
        
        public Player(Sprite sprite, Vector2 tilePosition, Texture2D invTexture, ContentManager content) : base(sprite, tilePosition)
        {
            inventory = new Inventory(invTexture, this);
            maxHealth = 100;
            maxMana = 100;
            health = maxHealth;  //should not be like that
            mana = maxMana;     //need to change it after a proper fighting implementation is done
            _destination = Sprite.Position;
            Texture2D weaponTexture = content.Load<Texture2D>("Items/mace");
            weapon = new Weapon(weaponTexture, "Sword", "A sword", ItemRarity.Common, 1, 3);
        }


        /// <summary>
        /// If player moves to the new position, remove the old position from the collision map.
        /// </summary>
        /// <param name="collisionMap"></param>
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

        /// <summary>
        /// If the player moves to the new position, add the new position to the collision map.
        /// </summary>
        /// <param name="playerTile"></param>
        /// <param name="collisionMap"></param>
        private void AddPlayerPositionToCollisionMap(Vector2 playerTile, Dictionary<Vector2, int> collisionMap)
        {
            collisionMap[playerTile] = collisionMap.ContainsKey(playerTile) && collisionMap[playerTile] > (int)Collision.NoCollision ? collisionMap[playerTile] : (int)Collision.PlayerCollision;
        }

        /// <summary>
        /// The method to Move the player character.
        /// <para>
        /// Depending on the state of the player character, the player will move in the direction of the key pressed.
        /// Movement is done by changing the position of the player character's sprite.
        /// However, movement always begins with the player character's sprite moving towards the center of the tile and doesn't stop until the sprite is in the center of the tile.
        /// This way, we can ensure that the player character's sprite is always aligned with the tile grid.
        /// No diagonall movement is allowed.
        /// </para>
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="inputController"></param>
        /// <param name="collisionMap"></param>
        public void MovePlayer(GameTime gameTime, InputController inputController, Dictionary<Vector2, int> collisionMap)
        {
            var newTilePosition = TilePosition;

            switch (State)
            {
                case CharacterState.Idle:
                    if (inputController.IsKeyDown(Keys.W) || inputController.IsKeyDown(Keys.Up))
                    {
                        newTilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize),
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize) - 1
                            );
                        if (!collisionMap.ContainsKey(newTilePosition) || collisionMap[newTilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingUp;
                            _destination.Y -= Globals.TileSize;
                            AddPlayerPositionToCollisionMap(newTilePosition, collisionMap);
                            TilePosition = newTilePosition;
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.S) || inputController.IsKeyDown(Keys.Down))
                    {
                        newTilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize),
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize) + 1
                            );
                        if (!collisionMap.ContainsKey(newTilePosition) || collisionMap[newTilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingDown;
                            _destination.Y += Globals.TileSize;
                            AddPlayerPositionToCollisionMap(newTilePosition, collisionMap);
                            TilePosition = newTilePosition;
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.A) || inputController.IsKeyDown(Keys.Left))
                    {
                        newTilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize) - 1,
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
                            );
                        if (!collisionMap.ContainsKey(newTilePosition) || collisionMap[newTilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingLeft;
                            _destination.X -= Globals.TileSize;
                            AddPlayerPositionToCollisionMap(newTilePosition, collisionMap);
                            TilePosition = newTilePosition;
                        }
                    }
                    else if (inputController.IsKeyDown(Keys.D) || inputController.IsKeyDown(Keys.Right))
                    {
                        newTilePosition = new Vector2(
                            (int)(Sprite.Position.X / Globals.TileSize) + 1,
                            (int)((Sprite.Position.Y + Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
                            );
                        if (!collisionMap.ContainsKey(newTilePosition) || collisionMap[newTilePosition] != 0)
                        {
                            RemoveOldPositionFromCollisionMap(collisionMap);
                            State = CharacterState.MovingRight;
                            _destination.X += Globals.TileSize;
                            AddPlayerPositionToCollisionMap(newTilePosition, collisionMap);
                            TilePosition = newTilePosition;
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
                enemy.health -= random.Next(1, 3);
            }
        }


        /// <summary>
        /// Updates the player character's logic.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="inputController"></param>
        /// <param name="collisionMap"></param>
        public void Update(GameTime gameTime, InputController inputController, Dictionary<Vector2, int> collisionMap)
        {
            MovePlayer(gameTime, inputController, collisionMap);
            if (inputController.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                inventory.showInventory = !inventory.showInventory;

            }
            if (inventory.showInventory)
            {
                inventory.Update();
            }
            //base.Update(gameTime);
        }

        /// <summary>
        /// Updates the player's tile and sprite position. The method is called during warp events.
        /// </summary>
        /// <param name="tilePosition"></param>
        public void UpdatePosition(Vector2 tilePosition)
        {
            TilePosition = tilePosition;
            Sprite.Position = new Vector2(
                TilePosition.X * Globals.TileSize + Globals.TileSize / 2 + Sprite.Texture.Width % Globals.TileSize,
                TilePosition.Y * Globals.TileSize - Sprite.Texture.Height % Globals.TileSize);
            _destination = Sprite.Position;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (weapon != null)
            {
                float scale = 0.3f;
                Vector2 weaponOffset = new Vector2(0, -2); 
                Vector2 weaponPosition = Sprite.Position + weaponOffset;

                spriteBatch.Draw(weapon.Texture, weaponPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
