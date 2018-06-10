using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.GeoVault;

namespace GeoElecMVC.Controllers
{
    public class VaultController : Controller
    {
        private readonly ManageGeoVault manageGeoVault;

        public VaultController(IConfiguration configuration)
        {
            manageGeoVault = new ManageGeoVault(configuration);
        }
        /*
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(manageGeoVault.FindAll());
        }
        */
        /*public async Task<IActionResult> Index(string searchString)
        {
            var testvault = from m in manageGeoVault.FindAll() select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                testvault = testvault.Where(s => s.generator_id.Equals(searchString));
            }

            return View(testvault.ToList());
        }
        */

        /*
        public IActionResult Index(string searchString, string timestamp, string checkDate)
        {
            //var testvault = from m in manageGeoVault.FindAll() select m;
            var testvault = manageGeoVault.FindAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                //testvault = testvault.Where(s => s.generator_id.Equals(searchString));
                //testvault = testvault.Where(s => s.generator_id.Contains(searchString));
                testvault = testvault.Where(s => s.generator_id.StartsWith(searchString));
            }


            if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate))
            {
                
                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);

                testvault = testvault.Where(x => x.log_date.Date <= parsedDateEnd);
                testvault = testvault.Where(y => y.log_date.Date >= parsedDateBegin);
            }

            return View(testvault.ToList());
        }
        */

        public IActionResult Index(string searchString, string timestamp, string checkDate)
        {
            ViewData["idGen"] = searchString;
            ViewData["timestamp"] = timestamp;
            //var testvault = from m in manageGeoVault.FindAll() select m;
            if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate) && !String.IsNullOrEmpty(searchString))
            {

                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);
                
                ViewData["nrjtot"] = (manageGeoVault.getNrjTot(searchString, parsedDateBegin, parsedDateEnd)).ToString();
                return View(manageGeoVault.getAllGenFram(searchString, parsedDateBegin, parsedDateEnd));
            }

            else if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate) && String.IsNullOrEmpty(searchString))
            {
                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);
                return View(manageGeoVault.getAllGenFram(parsedDateBegin, parsedDateEnd));
            }

            else if (!String.IsNullOrEmpty(checkDate))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    //testvault = testvault.Where(s => s.generator_id.Equals(searchString));
                    //testvault = testvault.Where(s => s.generator_id.Contains(searchString));
                    ViewData["nrjtot"] = (manageGeoVault.getNrjTot(searchString)).ToString();
                    return View(manageGeoVault.getAllGenFram(searchString));
                }
                return View(manageGeoVault.FindAll());
            }

            return View(manageGeoVault.getAllGenFram(DateTime.Now.AddDays(-30), DateTime.Now));
        }
    }
}
