using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StarterProject.Web.Api
{
    public class AIHelper
    {
        public AIHelper() { }

        public static string CreateStealAction(Point position)
        {
            return CreateAction("StealAction", position);
        }

        public static string CreateAttackAction(Point position)
        {
            return CreateAction("AttackAction", position);
        }

        public static string CreateCollectAction(Point position)
        {
            return CreateAction("CollectAction", position);
        }


        public static string CreateMoveAction(Point newPosition)
        {
            return CreateAction("MoveAction", newPosition);
        }

        public static string CreateUpgradeAction(UpgradeType upgrade)
        {
            return JsonConvert.SerializeObject(new ActionContent("UpgradeAction", upgrade) ); ;
        }

        public static string CreatePurchaseAction(PurchasableItem item)
        {
            return JsonConvert.SerializeObject(new ActionContent("PurchaseAction", item) ); ;
        }

        public static string CreateHealAction()
        {
            return JsonConvert.SerializeObject(new ActionContent() { ActionName = "HealAction" }); ;
        }

        private static string CreateAction(string name, Point target)
        {
            return JsonConvert.SerializeObject(new ActionContent(name, target));
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
