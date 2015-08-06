using ArkData;
using Microsoft.Framework.OptionsModel;
using System;

namespace ArkPlayerDataApi.Models
{
    public class PlayerRepository : IPlayerRepository
    {
        private ArkDataContainer _container;
        private ApplicationSettings _settings;

        public PlayerRepository(IOptions<ApplicationSettings> Configuration)
        {
            _settings = Configuration.Options;
        }
        
        public ArkDataContainer GetDataIncludingOnline()
        {
            _container = ArkDataContainer.Create(_settings.ArkDataDirectory);
            LoadOnlinePlayers();
            return _container;
        }

        public ArkDataContainer GetOfflineData()
        {
            return ArkDataContainer.Create(_settings.ArkDataDirectory);
        }

        public void LoadSteam()
        {
            _container.LoadSteam(_settings.SteamAPIKey);
        }

        private void LoadOnlinePlayers()
        {
            _container.LoadSteam(_settings.SteamAPIKey);
            var ip = _settings.ServerIP;
            var port = Convert.ToInt32(_settings.ServerPort);
            _container.LoadOnlinePlayers(ip, port);
        }
    }
}
