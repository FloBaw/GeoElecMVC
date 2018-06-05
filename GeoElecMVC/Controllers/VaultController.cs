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
        public IActionResult Index(string searchString)
        {
            var testvault = from m in manageGeoVault.FindAll() select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                testvault = testvault.Where(s => s.generator_id.Equals(searchString));
            }

            return View(testvault.ToList());
        }
    }
}
