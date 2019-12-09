using System.Collections.Generic;

namespace HouseDataFacade
{
    public class HouseDataService : IHouseDataService
    {
        private IHouseDataRepository _houseDataRepository;

        public HouseDataService( IHouseDataRepository houseDataRepository)
        {
            _houseDataRepository = houseDataRepository;
        }

        public HouseData GetSortedHouseInfo(List<House> baseHouseList = null)
        {
            return _houseDataRepository.GetSortedHouseInfo(baseHouseList);
        }
    }
}
