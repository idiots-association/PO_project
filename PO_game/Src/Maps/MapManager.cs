using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_game.Src.Maps
{
    public enum MapId
    {
        Lobby,
        HeroesPath,
        Dungeon
    }

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
