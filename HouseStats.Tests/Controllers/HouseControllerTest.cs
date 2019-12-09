using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseStats;
using HouseStats.Controllers;
using Unity;
using HouseDataFacade;

namespace HouseStats.Tests.Controllers
{
    [TestClass]
    public class HouseControllerTest
    {
        [TestMethod]
        public void GetHouseData()
        {
            // Arrange
            UnityContainer _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<IHouseDataRepository, HouseDataRepository>();
            _unityContainer.RegisterType<IHouseDataService, HouseDataService>();
            IHouseDataService dataService = _unityContainer.Resolve<HouseDataService>();

            // Act
            var result = (HouseData)dataService.GetSortedHouseInfo(PrepareHouseData());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.NearestHouse);
            Assert.IsTrue(result.NearestHouse.street == "Eberswalder Straße 55");
            Assert.IsTrue(result.SortedHouseByStreet.Count == 2);
            Assert.IsTrue(result.SortedHouseByRooms.Count == 6);
        }
   
        private List<House> PrepareHouseData()
        {
            List<House> LstHouse = new List<House>();
            LstHouse.Add(GetHouseObj("Adalbertstraße 13", 52.5013632, 13.4174913, 5, 1000000));
            LstHouse.Add(GetHouseObj("Brandenburgische Straße 10", 52.4888151, 13.3147011, 0, 1000000));
            LstHouse.Add(GetHouseObj("Cora-Berliner-Straße 22", 52.5141632, 13.3780111, 3, 1500000));
            LstHouse.Add(GetHouseObj("Danziger Straße 66", 52.53931, 13.4206011, 12, 5000000));
            LstHouse.Add(GetHouseObj("Eberswalder Straße 55", 52.5418739, 13.4057378, 10, 4000000));
            LstHouse.Add(GetHouseObj("Fehrbelliner Straße 23", 52.5336332, 13.4015613, 0, 0));
            LstHouse.Add(GetHouseObj("Gipsstraße 44", 52.5269281, 13.3984283, 20, 7000000));
            LstHouse.Add(GetHouseObj("Hermannstraße 1", 52.4858232, 13.4215013, 18, 2000000));
            LstHouse.Add(GetHouseObj("Innsbrucker Straße 8", 52.4863064, 13.3385237, 12, 2300000));
            LstHouse.Add(GetHouseObj("Jenaer Straße 8", 52.4896432, 13.3329913, 8, 800000));
            return LstHouse;
        }
        private  House GetHouseObj ( string streetName, double lat, double lon, int rooms, int value)
        {
            House hou = new House()
            {
                street = streetName,
                coords = new Coords()
                {
                    lat = lat,
                    lon = lon
                },
                @params = new Params()
                {
                    rooms = rooms,
                    value = value
                }
            };

            return hou;
        }
    }
}
