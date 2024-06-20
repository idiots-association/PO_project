<a name='assembly'></a>
# PO_game

## Contents

- [BattleScreen](#T-PO_game-Src-Screens-BattleScreen 'PO_game.Src.Screens.BattleScreen')
- [Button](#T-PO_game-Src-Controls-Button 'PO_game.Src.Controls.Button')
- [Camera](#T-PO_game-Src-Utils-Camera 'PO_game.Src.Utils.Camera')
  - [Follow()](#M-PO_game-Src-Utils-Camera-Follow-PO_game-Src-Entities-Player- 'PO_game.Src.Utils.Camera.Follow(PO_game.Src.Entities.Player)')
- [Character](#T-PO_game-Src-Entities-Character 'PO_game.Src.Entities.Character')
- [CharacterState](#T-PO_game-Src-Utils-CharacterState 'PO_game.Src.Utils.CharacterState')
- [Collision](#T-PO_game-Src-Utils-Collision 'PO_game.Src.Utils.Collision')
- [Consumable](#T-PO_game-Src-Items-Consumable 'PO_game.Src.Items.Consumable')
- [Enemy](#T-PO_game-Src-Entities-Enemy 'PO_game.Src.Entities.Enemy')
- [EnemyFactory](#T-PO_game-Src-Entities-EnemyFactory 'PO_game.Src.Entities.EnemyFactory')
- [Game1](#T-PO_game-Game1 'PO_game.Game1')
  - [#ctor()](#M-PO_game-Game1-#ctor 'PO_game.Game1.#ctor')
  - [ScreenManager](#P-PO_game-Game1-ScreenManager 'PO_game.Game1.ScreenManager')
  - [Draw(gameTime)](#M-PO_game-Game1-Draw-Microsoft-Xna-Framework-GameTime- 'PO_game.Game1.Draw(Microsoft.Xna.Framework.GameTime)')
  - [Initialize()](#M-PO_game-Game1-Initialize 'PO_game.Game1.Initialize')
  - [LoadContent()](#M-PO_game-Game1-LoadContent 'PO_game.Game1.LoadContent')
  - [Update(gameTime)](#M-PO_game-Game1-Update-Microsoft-Xna-Framework-GameTime- 'PO_game.Game1.Update(Microsoft.Xna.Framework.GameTime)')
- [GameScreen](#T-PO_game-Src-Screens-GameScreen 'PO_game.Src.Screens.GameScreen')
  - [Draw(spriteBatch)](#M-PO_game-Src-Screens-GameScreen-Draw-Microsoft-Xna-Framework-Graphics-SpriteBatch- 'PO_game.Src.Screens.GameScreen.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch)')
  - [Update(gameTime)](#M-PO_game-Src-Screens-GameScreen-Update-Microsoft-Xna-Framework-GameTime- 'PO_game.Src.Screens.GameScreen.Update(Microsoft.Xna.Framework.GameTime)')
- [Globals](#T-PO_game-Src-Utils-Globals 'PO_game.Src.Utils.Globals')
- [HealthPotion](#T-PO_game-Src-Items-Consumables-HealthPotion 'PO_game.Src.Items.Consumables.HealthPotion')
- [InputController](#T-PO_game-Src-Utils-InputController 'PO_game.Src.Utils.InputController')
- [Inventory](#T-PO_game-Src-Inv-Inventory 'PO_game.Src.Inv.Inventory')
- [InventorySlot](#T-PO_game-Src-Inv-InventorySlot 'PO_game.Src.Inv.InventorySlot')
- [Item](#T-PO_game-Src-Items-Item 'PO_game.Src.Items.Item')
- [ManaPotion](#T-PO_game-Src-Items-Consumables-ManaPotion 'PO_game.Src.Items.Consumables.ManaPotion')
- [Map](#T-PO_game-Src-Maps-Map 'PO_game.Src.Maps.Map')
  - [#ctor(csv_map,tileset,content)](#M-PO_game-Src-Maps-Map-#ctor-System-String,System-String,Microsoft-Xna-Framework-Content-ContentManager- 'PO_game.Src.Maps.Map.#ctor(System.String,System.String,Microsoft.Xna.Framework.Content.ContentManager)')
  - [CheckWarpCollision(player)](#M-PO_game-Src-Maps-Map-CheckWarpCollision-PO_game-Src-Entities-Player- 'PO_game.Src.Maps.Map.CheckWarpCollision(PO_game.Src.Entities.Player)')
  - [CreateEnemies(content)](#M-PO_game-Src-Maps-Map-CreateEnemies-Microsoft-Xna-Framework-Content-ContentManager- 'PO_game.Src.Maps.Map.CreateEnemies(Microsoft.Xna.Framework.Content.ContentManager)')
  - [Draw()](#M-PO_game-Src-Maps-Map-Draw-Microsoft-Xna-Framework-Graphics-SpriteBatch,PO_game-Src-Entities-Player- 'PO_game.Src.Maps.Map.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch,PO_game.Src.Entities.Player)')
  - [DrawLayer(layer,tileset,spriteBatch)](#M-PO_game-Src-Maps-Map-DrawLayer-System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32},Microsoft-Xna-Framework-Graphics-Texture2D,Microsoft-Xna-Framework-Graphics-SpriteBatch- 'PO_game.Src.Maps.Map.DrawLayer(System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32},Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Graphics.SpriteBatch)')
  - [GetWarpDestination(player)](#M-PO_game-Src-Maps-Map-GetWarpDestination-PO_game-Src-Entities-Player- 'PO_game.Src.Maps.Map.GetWarpDestination(PO_game.Src.Entities.Player)')
  - [LoadLayer(filename)](#M-PO_game-Src-Maps-Map-LoadLayer-System-String- 'PO_game.Src.Maps.Map.LoadLayer(System.String)')
  - [RemoveEnemy(enemy)](#M-PO_game-Src-Maps-Map-RemoveEnemy-PO_game-Src-Entities-Enemy- 'PO_game.Src.Maps.Map.RemoveEnemy(PO_game.Src.Entities.Enemy)')
  - [ShowCollisions(inputController)](#M-PO_game-Src-Maps-Map-ShowCollisions-PO_game-Src-Utils-InputController- 'PO_game.Src.Maps.Map.ShowCollisions(PO_game.Src.Utils.InputController)')
  - [Update(gameTime,inputController,player)](#M-PO_game-Src-Maps-Map-Update-Microsoft-Xna-Framework-GameTime,PO_game-Src-Utils-InputController,PO_game-Src-Entities-Player- 'PO_game.Src.Maps.Map.Update(Microsoft.Xna.Framework.GameTime,PO_game.Src.Utils.InputController,PO_game.Src.Entities.Player)')
  - [UpdateEnemyCollisions()](#M-PO_game-Src-Maps-Map-UpdateEnemyCollisions 'PO_game.Src.Maps.Map.UpdateEnemyCollisions')
- [MapId](#T-PO_game-Src-Maps-MapId 'PO_game.Src.Maps.MapId')
- [MapManager](#T-PO_game-Src-Maps-MapManager 'PO_game.Src.Maps.MapManager')
- [NPC](#T-PO_game-Src-Entities-NPC 'PO_game.Src.Entities.NPC')
- [Player](#T-PO_game-Src-Entities-Player 'PO_game.Src.Entities.Player')
  - [AddPlayerPositionToCollisionMap(playerTile,collisionMap)](#M-PO_game-Src-Entities-Player-AddPlayerPositionToCollisionMap-Microsoft-Xna-Framework-Vector2,System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}- 'PO_game.Src.Entities.Player.AddPlayerPositionToCollisionMap(Microsoft.Xna.Framework.Vector2,System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32})')
  - [MovePlayer(gameTime,inputController,collisionMap)](#M-PO_game-Src-Entities-Player-MovePlayer-Microsoft-Xna-Framework-GameTime,PO_game-Src-Utils-InputController,System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}- 'PO_game.Src.Entities.Player.MovePlayer(Microsoft.Xna.Framework.GameTime,PO_game.Src.Utils.InputController,System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32})')
  - [RemoveOldPositionFromCollisionMap(collisionMap)](#M-PO_game-Src-Entities-Player-RemoveOldPositionFromCollisionMap-System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}- 'PO_game.Src.Entities.Player.RemoveOldPositionFromCollisionMap(System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32})')
  - [Update(gameTime,inputController,collisionMap)](#M-PO_game-Src-Entities-Player-Update-Microsoft-Xna-Framework-GameTime,PO_game-Src-Utils-InputController,System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}- 'PO_game.Src.Entities.Player.Update(Microsoft.Xna.Framework.GameTime,PO_game.Src.Utils.InputController,System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32})')
  - [UpdatePosition(tilePosition)](#M-PO_game-Src-Entities-Player-UpdatePosition-Microsoft-Xna-Framework-Vector2- 'PO_game.Src.Entities.Player.UpdatePosition(Microsoft.Xna.Framework.Vector2)')
- [Program](#T-Program 'Program')
- [Screen](#T-PO_game-Src-Screens-Screen 'PO_game.Src.Screens.Screen')
- [ScreenManager](#T-PO_game-Src-Screens-ScreenManager 'PO_game.Src.Screens.ScreenManager')
- [Sprite](#T-PO_game-Src-Entities-Sprite 'PO_game.Src.Entities.Sprite')
- [Weapon](#T-PO_game-Src-Items-Weapon 'PO_game.Src.Items.Weapon')

<a name='T-PO_game-Src-Screens-BattleScreen'></a>
## BattleScreen `type`

##### Namespace

PO_game.Src.Screens

##### Summary

`BattleScreen` is a class that represents the battle screen in the game.

It allows the `Player` to fight an `Enemy` in a classical turn-based style.

<a name='T-PO_game-Src-Controls-Button'></a>
## Button `type`

##### Namespace

PO_game.Src.Controls

##### Summary

`Button` is a class that represents a button in the game.

It allows the creation of buttons with custom text, texture and different functions.

<a name='T-PO_game-Src-Utils-Camera'></a>
## Camera `type`

##### Namespace

PO_game.Src.Utils

##### Summary

`Camera` class to follow the player.

<a name='M-PO_game-Src-Utils-Camera-Follow-PO_game-Src-Entities-Player-'></a>
### Follow() `method`

##### Summary

`Camera` method to follow the player.

Follow takes `player` as a parameter and by using the `=player.Sprite.Position=player.Sprite.Position`, it sets the camera's position to follow the player, 
keeping the player in the center of the screen. [Sprite](#T-PO_game-Src-Entities-Sprite 'PO_game.Src.Entities.Sprite')

##### Parameters

This method has no parameters.

<a name='T-PO_game-Src-Entities-Character'></a>
## Character `type`

##### Namespace

PO_game.Src.Entities

##### Summary

`Character` is a class that represents a character in the game, be it player or NPC.

It allows the creation of characters with custom sprites, health, mana and position.

<a name='T-PO_game-Src-Utils-CharacterState'></a>
## CharacterState `type`

##### Namespace

PO_game.Src.Utils

##### Summary

Defines the possible states of a character.

<a name='T-PO_game-Src-Utils-Collision'></a>
## Collision `type`

##### Namespace

PO_game.Src.Utils

##### Summary

Defines the types of collision interactions.

<a name='T-PO_game-Src-Items-Consumable'></a>
## Consumable `type`

##### Namespace

PO_game.Src.Items

##### Summary

`Consumable` is an abstract class that represents a consumable item in the game.

It allows the creation of consumable items with custom textures, names, descriptions, rarities and quantities.

<a name='T-PO_game-Src-Entities-Enemy'></a>
## Enemy `type`

##### Namespace

PO_game.Src.Entities

##### Summary

`Enemy` is a class that represents an enemy character in the game.

It stores all the enemy's attributes, such as health, weapon, and agression.

<a name='T-PO_game-Src-Entities-EnemyFactory'></a>
## EnemyFactory `type`

##### Namespace

PO_game.Src.Entities

##### Summary

Class `EnemyFactory` is a class that creates enemies based on the type of enemy requested.
It stores all the pre-set enemy types and their respective textures and weapons.

<a name='T-PO_game-Game1'></a>
## Game1 `type`

##### Namespace

PO_game

##### Summary

`Game` class that initializes and runs the project.

<a name='M-PO_game-Game1-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructor of the Game1 class. Sets window settings and content directory.

##### Parameters

This constructor has no parameters.

<a name='P-PO_game-Game1-ScreenManager'></a>
### ScreenManager `property`

##### Summary

`ScreenManager` instance to manage screens of the game.

<a name='M-PO_game-Game1-Draw-Microsoft-Xna-Framework-GameTime-'></a>
### Draw(gameTime) `method`

##### Summary

Monogame Framework method to draw the game every frame.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameTime | [Microsoft.Xna.Framework.GameTime](#T-Microsoft-Xna-Framework-GameTime 'Microsoft.Xna.Framework.GameTime') |  |

<a name='M-PO_game-Game1-Initialize'></a>
### Initialize() `method`

##### Summary

Monogame Framework method to initialize the game properties. This method is called only once per instance.

##### Parameters

This method has no parameters.

<a name='M-PO_game-Game1-LoadContent'></a>
### LoadContent() `method`

##### Summary

Monogame Framework method to load content of the game. This method is called only once per instance.

##### Parameters

This method has no parameters.

<a name='M-PO_game-Game1-Update-Microsoft-Xna-Framework-GameTime-'></a>
### Update(gameTime) `method`

##### Summary

Monogame Framework method to update logic of the game every frame.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameTime | [Microsoft.Xna.Framework.GameTime](#T-Microsoft-Xna-Framework-GameTime 'Microsoft.Xna.Framework.GameTime') |  |

<a name='T-PO_game-Src-Screens-GameScreen'></a>
## GameScreen `type`

##### Namespace

PO_game.Src.Screens

##### Summary

`GameScreen` is a class handling the contents of the game.

<a name='M-PO_game-Src-Screens-GameScreen-Draw-Microsoft-Xna-Framework-Graphics-SpriteBatch-'></a>
### Draw(spriteBatch) `method`

##### Summary

Draw method called by Draw in Game1 class.

It calls the Draw method of the current map, the player inventory and the change state button.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| spriteBatch | [Microsoft.Xna.Framework.Graphics.SpriteBatch](#T-Microsoft-Xna-Framework-Graphics-SpriteBatch 'Microsoft.Xna.Framework.Graphics.SpriteBatch') |  |

<a name='M-PO_game-Src-Screens-GameScreen-Update-Microsoft-Xna-Framework-GameTime-'></a>
### Update(gameTime) `method`

##### Summary

Update method called by Update in Game1 class.

It handles input controller logic, all entities updates, camera following the player, map updates and button updates.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameTime | [Microsoft.Xna.Framework.GameTime](#T-Microsoft-Xna-Framework-GameTime 'Microsoft.Xna.Framework.GameTime') |  |

<a name='T-PO_game-Src-Utils-Globals'></a>
## Globals `type`

##### Namespace

PO_game.Src.Utils

##### Summary

Contains global settings and properties used throughout the game.

<a name='T-PO_game-Src-Items-Consumables-HealthPotion'></a>
## HealthPotion `type`

##### Namespace

PO_game.Src.Items.Consumables

##### Summary

`HealthPotion` is a class that represents a health potion in the game.

It allows the creation of health potions with custom health values.

<a name='T-PO_game-Src-Utils-InputController'></a>
## InputController `type`

##### Namespace

PO_game.Src.Utils

##### Summary

`InputController` class to manage input from the keyboard.

<a name='T-PO_game-Src-Inv-Inventory'></a>
## Inventory `type`

##### Namespace

PO_game.Src.Inv

##### Summary

`Inventory` is a class that represents an inventory in the game.

It allows the creation of inventories with custom textures and player references and consists of 36 `InventorySlots.``Consumable` items are stackable, `Weapon`s are not.

<a name='T-PO_game-Src-Inv-InventorySlot'></a>
## InventorySlot `type`

##### Namespace

PO_game.Src.Inv

##### Summary

`InventorySlot` is a class that represents an inventory slot in the game.

It allows the creation of inventory slots with custom textures and items and allows the player to interact with the items in the inventory.
Any `InventorySlot` can hold only one type of an item at a time.

<a name='T-PO_game-Src-Items-Item'></a>
## Item `type`

##### Namespace

PO_game.Src.Items

##### Summary

`Item` is an abstract class that represents an item in the game.

It allows the creation of items with custom textures, names, descriptions and rarities.

<a name='T-PO_game-Src-Items-Consumables-ManaPotion'></a>
## ManaPotion `type`

##### Namespace

PO_game.Src.Items.Consumables

##### Summary

`ManaPotion` is a class that represents a mana potion in the game.

It allows the creation of mana potions with custom mana values.

<a name='T-PO_game-Src-Maps-Map'></a>
## Map `type`

##### Namespace

PO_game.Src.Maps

##### Summary

`Map` class to manage the map of the game.

<a name='M-PO_game-Src-Maps-Map-#ctor-System-String,System-String,Microsoft-Xna-Framework-Content-ContentManager-'></a>
### #ctor(csv_map,tileset,content) `constructor`

##### Summary

`Map` constructor. It loads the map from the csvs files and the tileset from the content.

Enemy csv file is optional. 
If it exists, it creates enemies from the csv file and calls the `UpdateEnemyCollisions` method to add enemies collisions to the _collisions map.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| csv_map | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tileset | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| content | [Microsoft.Xna.Framework.Content.ContentManager](#T-Microsoft-Xna-Framework-Content-ContentManager 'Microsoft.Xna.Framework.Content.ContentManager') |  |

<a name='M-PO_game-Src-Maps-Map-CheckWarpCollision-PO_game-Src-Entities-Player-'></a>
### CheckWarpCollision(player) `method`

##### Summary

Method called in the Update method to check if the player has collided with a warp.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| player | [PO_game.Src.Entities.Player](#T-PO_game-Src-Entities-Player 'PO_game.Src.Entities.Player') |  |

<a name='M-PO_game-Src-Maps-Map-CreateEnemies-Microsoft-Xna-Framework-Content-ContentManager-'></a>
### CreateEnemies(content) `method`

##### Summary

A method to create enemies from the csv file.

##### Returns

List of `Enemy objects associated with this map.`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| content | [Microsoft.Xna.Framework.Content.ContentManager](#T-Microsoft-Xna-Framework-Content-ContentManager 'Microsoft.Xna.Framework.Content.ContentManager') |  |

<a name='M-PO_game-Src-Maps-Map-Draw-Microsoft-Xna-Framework-Graphics-SpriteBatch,PO_game-Src-Entities-Player-'></a>
### Draw() `method`

##### Summary

A method to draw the map. It draws the background, collisions (if enabled) and the enteties of the map (including player, enemies and NPCs).

##### Parameters

This method has no parameters.

<a name='M-PO_game-Src-Maps-Map-DrawLayer-System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32},Microsoft-Xna-Framework-Graphics-Texture2D,Microsoft-Xna-Framework-Graphics-SpriteBatch-'></a>
### DrawLayer(layer,tileset,spriteBatch) `method`

##### Summary

A method to draw a layer of the map. It uses the `_tileset` to draw the tiles.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| layer | [System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}') |  |
| tileset | [Microsoft.Xna.Framework.Graphics.Texture2D](#T-Microsoft-Xna-Framework-Graphics-Texture2D 'Microsoft.Xna.Framework.Graphics.Texture2D') |  |
| spriteBatch | [Microsoft.Xna.Framework.Graphics.SpriteBatch](#T-Microsoft-Xna-Framework-Graphics-SpriteBatch 'Microsoft.Xna.Framework.Graphics.SpriteBatch') |  |

<a name='M-PO_game-Src-Maps-Map-GetWarpDestination-PO_game-Src-Entities-Player-'></a>
### GetWarpDestination(player) `method`

##### Summary

A method to get the destination of a warp using the player's position.

##### Returns

Destination of a warp

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| player | [PO_game.Src.Entities.Player](#T-PO_game-Src-Entities-Player 'PO_game.Src.Entities.Player') |  |

<a name='M-PO_game-Src-Maps-Map-LoadLayer-System-String-'></a>
### LoadLayer(filename) `method`

##### Summary

Method to load a layer from a csv file.

##### Returns

The dictionary witch contains a tile position of an object paired with some number associated with the object type.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| filename | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-PO_game-Src-Maps-Map-RemoveEnemy-PO_game-Src-Entities-Enemy-'></a>
### RemoveEnemy(enemy) `method`

##### Summary

Method to remove enemy from the map after it's defeated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| enemy | [PO_game.Src.Entities.Enemy](#T-PO_game-Src-Entities-Enemy 'PO_game.Src.Entities.Enemy') |  |

<a name='M-PO_game-Src-Maps-Map-ShowCollisions-PO_game-Src-Utils-InputController-'></a>
### ShowCollisions(inputController) `method`

##### Summary

A method to show collisions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inputController | [PO_game.Src.Utils.InputController](#T-PO_game-Src-Utils-InputController 'PO_game.Src.Utils.InputController') |  |

<a name='M-PO_game-Src-Maps-Map-Update-Microsoft-Xna-Framework-GameTime,PO_game-Src-Utils-InputController,PO_game-Src-Entities-Player-'></a>
### Update(gameTime,inputController,player) `method`

##### Summary

Map Update method. Handles the collisions, warps and enemies logic and is called by the Update method in the `GameScreen` class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameTime | [Microsoft.Xna.Framework.GameTime](#T-Microsoft-Xna-Framework-GameTime 'Microsoft.Xna.Framework.GameTime') |  |
| inputController | [PO_game.Src.Utils.InputController](#T-PO_game-Src-Utils-InputController 'PO_game.Src.Utils.InputController') |  |
| player | [PO_game.Src.Entities.Player](#T-PO_game-Src-Entities-Player 'PO_game.Src.Entities.Player') |  |

<a name='M-PO_game-Src-Maps-Map-UpdateEnemyCollisions'></a>
### UpdateEnemyCollisions() `method`

##### Summary

A method to update the collisions map with the enemies collisions. Called after creating enemies.

##### Parameters

This method has no parameters.

<a name='T-PO_game-Src-Maps-MapId'></a>
## MapId `type`

##### Namespace

PO_game.Src.Maps

##### Summary

Enum representing the different maps in the game.

<a name='T-PO_game-Src-Maps-MapManager'></a>
## MapManager `type`

##### Namespace

PO_game.Src.Maps

##### Summary

`MapManager` class manages the maps in the game.

<a name='T-PO_game-Src-Entities-NPC'></a>
## NPC `type`

##### Namespace

PO_game.Src.Entities

##### Summary

`NPC` is a class that represents a non-playable friendly character in the game.

Doesn't do much for now.

<a name='T-PO_game-Src-Entities-Player'></a>
## Player `type`

##### Namespace

PO_game.Src.Entities

##### Summary

`Player` is a class that represents the player character in the game.

The player class stores all the player's attributes, such as health, mana, weapon and inventory and some movement and collision logic.

<a name='M-PO_game-Src-Entities-Player-AddPlayerPositionToCollisionMap-Microsoft-Xna-Framework-Vector2,System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}-'></a>
### AddPlayerPositionToCollisionMap(playerTile,collisionMap) `method`

##### Summary

If the player moves to the new position, add the new position to the collision map.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| playerTile | [Microsoft.Xna.Framework.Vector2](#T-Microsoft-Xna-Framework-Vector2 'Microsoft.Xna.Framework.Vector2') |  |
| collisionMap | [System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}') |  |

<a name='M-PO_game-Src-Entities-Player-MovePlayer-Microsoft-Xna-Framework-GameTime,PO_game-Src-Utils-InputController,System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}-'></a>
### MovePlayer(gameTime,inputController,collisionMap) `method`

##### Summary

The method to Move the player character.

Depending on the state of the player character, the player will move in the direction of the key pressed.
Movement is done by changing the position of the player character's sprite.
However, movement always begins with the player character's sprite moving towards the center of the tile and doesn't stop until the sprite is in the center of the tile.
This way, we can ensure that the player character's sprite is always aligned with the tile grid.
No diagonall movement is allowed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameTime | [Microsoft.Xna.Framework.GameTime](#T-Microsoft-Xna-Framework-GameTime 'Microsoft.Xna.Framework.GameTime') |  |
| inputController | [PO_game.Src.Utils.InputController](#T-PO_game-Src-Utils-InputController 'PO_game.Src.Utils.InputController') |  |
| collisionMap | [System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}') |  |

<a name='M-PO_game-Src-Entities-Player-RemoveOldPositionFromCollisionMap-System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}-'></a>
### RemoveOldPositionFromCollisionMap(collisionMap) `method`

##### Summary

If player moves to the new position, remove the old position from the collision map.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collisionMap | [System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}') |  |

<a name='M-PO_game-Src-Entities-Player-Update-Microsoft-Xna-Framework-GameTime,PO_game-Src-Utils-InputController,System-Collections-Generic-Dictionary{Microsoft-Xna-Framework-Vector2,System-Int32}-'></a>
### Update(gameTime,inputController,collisionMap) `method`

##### Summary

Updates the player character's logic.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameTime | [Microsoft.Xna.Framework.GameTime](#T-Microsoft-Xna-Framework-GameTime 'Microsoft.Xna.Framework.GameTime') |  |
| inputController | [PO_game.Src.Utils.InputController](#T-PO_game-Src-Utils-InputController 'PO_game.Src.Utils.InputController') |  |
| collisionMap | [System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{Microsoft.Xna.Framework.Vector2,System.Int32}') |  |

<a name='M-PO_game-Src-Entities-Player-UpdatePosition-Microsoft-Xna-Framework-Vector2-'></a>
### UpdatePosition(tilePosition) `method`

##### Summary

Updates the player's tile and sprite position. The method is called during warp events.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tilePosition | [Microsoft.Xna.Framework.Vector2](#T-Microsoft-Xna-Framework-Vector2 'Microsoft.Xna.Framework.Vector2') |  |

<a name='T-Program'></a>
## Program `type`

##### Namespace



##### Summary

Initializes and runs project.

<a name='T-PO_game-Src-Screens-Screen'></a>
## Screen `type`

##### Namespace

PO_game.Src.Screens

##### Summary

`Screen` is an abstract class creating interface for all the other Screen classes.

<a name='T-PO_game-Src-Screens-ScreenManager'></a>
## ScreenManager `type`

##### Namespace

PO_game.Src.Screens

##### Summary

`ScreenManager` class to manage screens.

It contains instance of itself to ensure so that there is only one Screen Manager per build.

<a name='T-PO_game-Src-Entities-Sprite'></a>
## Sprite `type`

##### Namespace

PO_game.Src.Entities

##### Summary

`Sprite` is a class that represents a sprite in the game.

It allows the creation of sprites with custom textures and positions.

<a name='T-PO_game-Src-Items-Weapon'></a>
## Weapon `type`

##### Namespace

PO_game.Src.Items

##### Summary

`Weapon` is a class that represents a weapon in the game.

It allows the creation of weapons with custom damage values.
