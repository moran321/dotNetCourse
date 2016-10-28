using PriceCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using WebApplication1.EF;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/item")]
    public class ItemsController : ApiController
    {
        private DataGetter _dataGetter;
        public ItemsController()
        {
            _dataGetter = new DataGetter();
        }

        [HttpGet]
        [Route("GetItem")]
        public IHttpActionResult GetItem()
        {
            var item = _dataGetter.GetChains();
            var result = Json(item.Select(i => i.Name).ToList());
            return result;
            //return Ok(item);
        }
    }


    //public class ItemsController : Controller
    //{
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    public JsonResult GetChains()
    //    {
    //        PricesContext dal = new PricesContext();
    //        List<Chain> list_of_chains = dal.Chains.ToList();
    //        return Json(list_of_chains, JsonRequestBehavior.AllowGet);
    //    }
    //}
}
