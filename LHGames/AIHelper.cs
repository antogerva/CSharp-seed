using Newtonsoft.Json;

namespace LHGames
{
    public class AiHelper
    {
        public AiHelper()
        {
            
        }

        public string GetText()
        {
            return "HELLO COUCOU BITCH";
        }

        public static string TestDeplacement(GameInfo gameInfo, Tile[,] map)
        {
            var currentPos = new Point(gameInfo.Player.Position.X, gameInfo.Player.Position.Y);
            Point posInTileMap = null;
            bool leave = false;
            for (int i = 0; i < map.Length && !leave; i++)
            {
                for (int j = 0; j < map.Length; j++)
                {
                    if (map[i, j].X == currentPos.X && map[i, j].Y == currentPos.Y)
                    {
                        posInTileMap = new Point(i, j);
                        leave = true;
                        break;
                    }
                }
            }
            Point nextPos = new Point(map[posInTileMap.X, posInTileMap.Y].X, map[posInTileMap.X, posInTileMap.Y].Y);
            if (posInTileMap.X + 1 < map.Length && (TileContent)(map[posInTileMap.X + 1, posInTileMap.Y].C) == TileContent.Empty)
                nextPos = new Point(map[posInTileMap.X + 1, posInTileMap.Y].X, map[posInTileMap.X + 1, posInTileMap.Y].Y);
            else if (posInTileMap.X - 1 >= 0 && (TileContent)(map[posInTileMap.X - 1, posInTileMap.Y].C) == TileContent.Empty)
                nextPos = new Point(map[posInTileMap.X - 1, posInTileMap.Y].X, map[posInTileMap.X - 1, posInTileMap.Y].Y);
            else if (posInTileMap.Y + 1 < map.Length && (TileContent)(map[posInTileMap.X, posInTileMap.Y + 1].C) == TileContent.Empty)
                nextPos = new Point(map[posInTileMap.X, posInTileMap.Y + 1].X, map[posInTileMap.X, posInTileMap.Y + 1].Y);
            else if (posInTileMap.Y - 1 >= 0 && (TileContent)(map[posInTileMap.X, posInTileMap.Y - 1].C) == TileContent.Empty)
                nextPos = new Point(map[posInTileMap.X, posInTileMap.Y - 1].X, map[posInTileMap.X, posInTileMap.Y - 1].Y);

            return CreateMoveAction(nextPos);
        }

        public static string CreateStealAction(Point position) {
            return CreateAction("StealAction", position);
        }

        public static string CreateAttackAction(Point position) {
            return CreateAction("AttackAction", position);
        }

        public static string CreateCollectAction(Point position) {
            return CreateAction("CollectAction", position);
        }


        public static string CreateMoveAction(Point newPosition) {
            return CreateAction("MoveAction", newPosition);
        }

        private static string CreateAction(string name, Point target) {
            string action = JsonConvert.SerializeObject(new ActionContent(name, target));
            return action;
        }

        public static Tile[,] DeserializeMap(string customSerializedMap)
        {
            customSerializedMap = customSerializedMap.Substring(1, customSerializedMap.Length - 1);
            var rows = customSerializedMap.Split('[');
            var column = rows[1].Split('{');
            var map = new Tile[rows.Length - 1, column.Length - 1];
            for (int i = 0; i < rows.Length - 1; i++)
            {
                column = rows[i + 1].Split('{');
                for (int j = 0; j < column.Length - 1; j++)
                {
                    var infos = column[j + 1].Split(',');
                    map[i, j] = new Tile(byte.Parse(infos[0]), int.Parse(infos[1]), int.Parse(infos[2].Substring(0, infos[2].IndexOf('}'))));
                }
            }
            return map;
        }

    }
}
