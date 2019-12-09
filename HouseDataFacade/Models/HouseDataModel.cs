using System;
using System.Collections.Generic;

namespace HouseDataFacade
{
    public class Coords
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Params
    {
        public int rooms { get; set; }
        public int value { get; set; }
    }

    public class House
    {
        public Coords coords { get; set; }
        public Params @params { get; set; }
        public string street { get; set; }
        public double distance { get; set; }

        public HouseInfo GetHouseInfo()
        {
            return new HouseInfo()
            {
                Street = street,
                Rooms = @params == null ? 0 : @params.rooms,
                Value = @params == null ? 0 : @params.value,
                Lat = coords.lat,
                Lon = coords.lon,
                Distance = distance
            };
            
        }
    }
    public class RootObject
    {
        public List<House> houses { get; set; }
    }

    public class HouseInfo
    {
        public string Street { get; set; }
        public int  Rooms { get; set; }
        public int Value { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Distance { get; set; }
                
    }

    public class HouseData
    {
        public List<HouseInfo> SourceData { get; set; }
        public List<HouseInfo> SortedHouseByDistance { get; set; }
        public List<HouseInfo> SortedHouseByRooms { get; set; }
        public List<HouseInfo> SortedHouseByStreet { get; set; }
        public HouseInfo NearestHouse { get; set; }
            
        public HouseData()
        {
            SourceData = new List<HouseInfo>();
            SortedHouseByDistance = new List<HouseInfo>(); 
            SortedHouseByRooms = new List<HouseInfo>();
            SortedHouseByStreet = new List<HouseInfo>();
        }

    }
}
