using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.GeoVault;
using System.Security.Claims;

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
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchString, string timestamp, string checkDate)
        {
            ViewData["idGen"] = searchString;
            ViewData["timestamp"] = timestamp;
            var ident = User.Identity as ClaimsIdentity;
            var userId = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //ViewData["userId"] = userId;
            //var testvault = from m in manageGeoVault.FindAll() select m;
            if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate) && !String.IsNullOrEmpty(searchString))
            {

                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);
                
                ViewData["nrjtot"] = (manageGeoVault.getNrjTot(searchString, parsedDateBegin, parsedDateEnd)).ToString();
                return View(manageGeoVault.FindAllGenFram(searchString, parsedDateBegin, parsedDateEnd));
            }

            else if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate) && String.IsNullOrEmpty(searchString))
            {
                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);
                return View(manageGeoVault.FindAllGenFram(parsedDateBegin, parsedDateEnd));
            }

            else if (!String.IsNullOrEmpty(checkDate))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    //testvault = testvault.Where(s => s.generator_id.Equals(searchString));
                    //testvault = testvault.Where(s => s.generator_id.Contains(searchString));
                    ViewData["nrjtot"] = (manageGeoVault.getNrjTot(searchString)).ToString();
                    return View(manageGeoVault.FindAllGenFram(searchString));
                }
                return View(manageGeoVault.FindAllGenFram());
            }

            return View(manageGeoVault.FindAllGenFram(DateTime.Now.AddDays(-30), DateTime.Now));
        }


        [Authorize(Roles = "Admin, Member")]
        public IActionResult IndexMember(string searchString, string timestamp, string checkDate)
        {
            ViewData["idGen"] = searchString;
            ViewData["timestamp"] = timestamp;
            var ident = User.Identity as ClaimsIdentity;
            var userId = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //ViewData["userId"] = userId;
            if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate) && !String.IsNullOrEmpty(searchString))
            {

                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);

                ViewData["nrjtot"] = (manageGeoVault.getItsNrjTot(userId, searchString, parsedDateBegin, parsedDateEnd)).ToString();
                return View(manageGeoVault.FindAllItsGenFram(userId, searchString, parsedDateBegin, parsedDateEnd));
            }

            else if (!String.IsNullOrEmpty(timestamp) && String.IsNullOrEmpty(checkDate) && String.IsNullOrEmpty(searchString))
            {
                string[] splitDate = timestamp.Split('-');
                string dateBegin = splitDate[0];
                string dateEnd = splitDate[1];
                DateTime parsedDateBegin = DateTime.Parse(dateBegin);
                DateTime parsedDateEnd = DateTime.Parse(dateEnd);
                return View(manageGeoVault.FindAllItsGenFram(userId, parsedDateBegin, parsedDateEnd));
            }

            else if (!String.IsNullOrEmpty(checkDate))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    //testvault = testvault.Where(s => s.generator_id.Equals(searchString));
                    //testvault = testvault.Where(s => s.generator_id.Contains(searchString));
                    ViewData["nrjtot"] = (manageGeoVault.getItsNrjTot(userId, searchString)).ToString();
                    return View(manageGeoVault.FindAllItsGenFram(userId, searchString));
                }
                return View(manageGeoVault.FindAllItsGenFram(userId));
            }

            return View(manageGeoVault.FindAllItsGenFram(userId, DateTime.Now.AddDays(-30), DateTime.Now));
        }
    }
}
