using System.Collections.Generic;

namespace HouseDataFacade
{
    public interface IHouseDataRepository
    {
        HouseData GetSortedHouseInfo(List<House> baseHouseList = null);
    }
}
