using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.WebClient.Models;

namespace PriceCompare.WebClient.Controllers
{
    public class ChainsController : Controller
    {
        // GET: Chains
        public ActionResult Index()
        {
            ChainClient c = new ChainClient();
            ViewBag.ChainsList = c.findAll();
            return View();
        }
    }
}