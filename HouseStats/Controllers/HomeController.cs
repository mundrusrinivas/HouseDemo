using HouseDataFacade;
using System.Web.Mvc;

namespace HouseStats.Controllers
{
    public class HomeController : Controller
    {
        private IHouseDataService _houseDataService;

        public HomeController(IHouseDataService houseDataService)
        {
            _houseDataService = houseDataService;
        }
        public ActionResult Index()
        {
            return View(_houseDataService.GetSortedHouseInfo());
        }
    }
}