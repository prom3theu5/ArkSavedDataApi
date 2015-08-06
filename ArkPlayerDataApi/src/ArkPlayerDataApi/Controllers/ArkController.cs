using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using ArkPlayerDataApi.Models;
using ArkData;

namespace ArkPlayerDataApi.Controllers
{
    [Route("api/[controller]")]
    public class ArkController : Controller
    {
        private readonly IPlayerRepository _playerRepo;

        public ArkController(IPlayerRepository PlayerRepo)
        {
            _playerRepo = PlayerRepo;
        }

        // GET: api/ark/GetOnlineData
        [HttpGet]
        [Route("GetOnlineData")]
        public ArkDataContainer GetOnlineData()
        {
            return _playerRepo.GetDataIncludingOnline();
        }

        // GET: api/ark/GetOfflineData
        [HttpGet]
        [Route("GetOfflineData")]
        public ArkDataContainer GetOfflineData()
        {
            return _playerRepo.GetOfflineData();
        }

        // GET: api/ark/GetPlayers
        [HttpGet]
        [Route("GetPlayers")]
        public List<Player> GetPlayers()
        {
            return _playerRepo.GetOfflineData().Players;
        }

        // GET: api/ark/GetTribes
        [HttpGet]
        [Route("GetTribes")]
        public List<Tribe> GetTribes()
        {
            return _playerRepo.GetOfflineData().Tribes;
        }
    }
}
