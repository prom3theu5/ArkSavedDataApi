using ArkData;

namespace ArkPlayerDataApi.Models
{
    public interface IPlayerRepository
    {
        ArkDataContainer GetDataIncludingOnline();
        ArkDataContainer GetOfflineData();
    }
}
