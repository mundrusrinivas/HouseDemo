using System.Collections.Generic;

namespace HouseDataFacade
{
    public interface IHouseDataService
    {
        HouseData GetSortedHouseInfo(List<House> baseHouseList = null);
    }
}
