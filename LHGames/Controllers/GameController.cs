using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LHGames.Controllers
{
    [Route("")]
    public class GameController : Controller
    {
        AiHelper player = new AiHelper();

        //POST 
        [HttpPost]
        public string Index([FromBody]string map)
        {
            Message mapData = JsonConvert.DeserializeObject<Message>(map);
            GameInfo gameInfo = JsonConvert.DeserializeObject<GameInfo>(mapData.Content);
            var carte = AiHelper.DeserializeMap(gameInfo.CustomSerializedMap);
            
            string testDeplacement = AiHelper.TestDeplacement(gameInfo, carte);
            return testDeplacement;
        }


        [HttpGet("")]
        public string Index()
        {
            try
            {
                return player.GetText();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
