﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace PO_game.Src.Maps
{
    /// <summary>
    /// Enum representing the different maps in the game.
    /// </summary>
    public enum MapId
    {
        Lobby,
        PlayerPath,
        DarkForest
    }


    /// <summary>
    /// <c>Warps</c>> class contains the warp points for each map.
    /// </summary>
    public static class Warps
    {
        public static Dictionary<MapId, Dictionary<Vector2, Tuple<MapId, Vector2>>> WarpPoints = new Dictionary<MapId, Dictionary<Vector2, Tuple<MapId, Vector2>>>()
    {
        {
            MapId.Lobby, new Dictionary<Vector2, Tuple<MapId, Vector2>>()
            {
                { new Vector2(20, 8), new Tuple<MapId, Vector2>(MapId.PlayerPath, new Vector2(3, 9)) },
            }
        },
        {
            MapId.PlayerPath, new Dictionary<Vector2, Tuple<MapId, Vector2>>()
            {
                { new Vector2(8, 5), new Tuple<MapId, Vector2>(MapId.DarkForest, new Vector2(20, 10)) },
                { new Vector2(8, 9), new Tuple<MapId, Vector2>(MapId.DarkForest, new Vector2(20, 10)) },
                { new Vector2(8, 13), new Tuple<MapId, Vector2>(MapId.DarkForest, new Vector2(20, 10)) },
            }
        },
    };
    }


    /// <summary>
    /// <c>MapManager</c> class manages the maps in the game.
    /// </summary>
    public class MapManager
    {

        private static MapManager _instance;
        private Dictionary<MapId, Map> _maps;
        public MapId CurrentMap { get; private set; }

        private MapManager()
        {
            _maps = new Dictionary<MapId, Map>();
        }

        public static MapManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MapManager();
                }
                return _instance;
            }
        }

        public void AddMap(MapId mapId, Map map)
        {
            _maps.Add(mapId, map);
        }

        public Map GetMap(MapId mapId)
        {
            return _maps[mapId];
        }

        public Map GetCurrentMap()
        {
            return _maps[CurrentMap];
        }   

        public void SetCurrentMap(MapId mapId)
        {
            if (_maps.ContainsKey(mapId))
            {
                CurrentMap = mapId;
            }
            else
            {
                throw new Exception($"Map with ID {mapId} does not exist.");
            }
        }

        public void ClearMaps()
        {
            _maps.Clear();
        }
    }
}
