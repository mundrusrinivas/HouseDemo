using GoogleMaps.LocationServices;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Device.Location;
using System.Net.Http;

namespace HouseDataFacade
{
    public class HouseDataRepository : IHouseDataRepository
    {
        private const string _HouseUrl = "HouseDataUrl";
        private const string _SisterAddress = "SisterAddress";
        private const string _GoogleKey = "GoogleKey";

        /// <summary>
        /// This function will return 
        ///              Sorted Houses by Distance
        ///              Sorted Houses by Rooms with min 5 rooms 
        ///              Sorted Houses by Street if room info misses
        ///              Get Nearest house from sister house and with more rooms with 10 rooms and 5000000 value
        /// </summary>
        /// <param name="baseHouseList"></param>
        /// <returns></returns>
        public HouseData GetSortedHouseInfo(List<House> baseHouseList = null)
        {
            if (baseHouseList == null)
            {
                baseHouseList = GetListofHouses();
            }
            HouseData houseData = new HouseData();
    
            var baseCord = PopulateHouseCoordsBasedOnAddress();

            foreach (var curHouse in baseHouseList)
            {
                var eCoord = new GeoCoordinate(curHouse.coords.lat, curHouse.coords.lon);
                curHouse.distance = baseCord.GetDistanceTo(eCoord);

                HouseInfo houseInfo = curHouse.GetHouseInfo();
                houseData.SourceData.Add(houseInfo);

                PopulateSortedHouseByDistance(houseData, houseInfo);

                //Indicates we don't have full information so sort them by Street
                if (curHouse.@params == null || curHouse.@params.rooms <= 0 || curHouse.@params.value <= 0)
                {
                    PopulateSortedHouseByStreet(houseData, houseInfo);
                }
                else //Indicates we have full information so sort them by rooms
                {
                    PopulateSortedHouseByRooms(houseData, houseInfo);
                    PopulateNearestHouse(houseData, houseInfo);
                }
            }
            houseData.SortedHouseByDistance.Sort((curr, comp) => curr.Distance.CompareTo(comp.Distance));
            houseData.SortedHouseByRooms.Sort((curr, comp) => curr.Rooms.CompareTo(comp.Rooms));
            houseData.SortedHouseByStreet.Sort((curr, comp) => curr.Street.CompareTo(comp.Street));
            return houseData;
        }
        /// <summary>
        /// Add it to Sorted list
        /// </summary>
        /// <param name="houseData"></param>
        /// <param name="curHouse"></param>
        private void PopulateSortedHouseByDistance(HouseData houseData, HouseInfo curHouse)
        {
            houseData.SortedHouseByDistance.Add(curHouse);
        }

        /// <summary>
        /// Add it to Sorted list
        /// </summary>
        /// <param name="houseData"></param>
        /// <param name="curHouse"></param>
        private void PopulateSortedHouseByStreet(HouseData houseData, HouseInfo curHouse)
        {
            houseData.SortedHouseByStreet.Add(curHouse);
        }
        /// <summary>
        /// Populate nearest house by distance
        /// </summary>
        /// <param name="houseData"></param>
        /// <param name="curHouse"></param>
        private void PopulateNearestHouse(HouseData houseData, HouseInfo curHouse)
        {
            //Filter with number of rooms and value.
            if (curHouse.Rooms >= 10 && curHouse.Value <= 5000000)
            {
                houseData.NearestHouse = houseData.NearestHouse == null ? curHouse : (houseData.NearestHouse.Distance <= curHouse.Distance) ? houseData.NearestHouse : curHouse;
            }
        }

        /// <summary>
        /// Add it to Sorted list if rooms greater than 5
        /// </summary>
        /// <param name="houseData"></param>
        /// <param name="curHouse"></param>
        private void PopulateSortedHouseByRooms(HouseData houseData, HouseInfo curHouse)
        {
            if (curHouse.Rooms > 5)
            {
                houseData.SortedHouseByRooms.Add(curHouse);
            }
        }
        /// <summary>
        /// Get list of houses from House web api
        /// </summary>
        /// <returns></returns>
        private List<House> GetListofHouses()
        {
            string houseUrl = ConfigurationManager.AppSettings[_HouseUrl].ToString();
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(houseUrl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<RootObject>();
                    readTask.Wait();

                    return readTask.Result.houses;
                }
            }
            return null;
        }

        private GeoCoordinate PopulateHouseCoordsBasedOnAddress()
        {
            var address = ConfigurationManager.AppSettings[_SisterAddress].ToString();
            var key = ConfigurationManager.AppSettings[_GoogleKey].ToString();
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={key}";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<AddressRootObject>();
                    readTask.Wait();

                    Location loc = null;
                    if (readTask.Result.status == "OK")
                    {
                         loc = readTask.Result.results[0].geometry.location;
                    }
                    else
                    {
                        loc = new Location()
                        {
                            lat = 52.5418707,
                            lng = 13.4079265
                        };
                    }
                    return new GeoCoordinate(loc.lat, loc.lng);
                }
            }
            return null;
        }
               
    }
}
