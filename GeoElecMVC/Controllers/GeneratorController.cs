using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.GeoGenerator;
using GeoElecMVC.SuperAdminCustomer;
using System.Security.Claims;

namespace GeoElecMVC.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly ManageGeoGenerator manageGeoGenerator;
        private readonly ManageSuperAdminCustomer manageSuperAdminCustomer;

        public GeneratorController(IConfiguration configuration)
        {
            manageGeoGenerator = new ManageGeoGenerator(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageGeoGenerator.FindAllGen());
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Generator obj = manageGeoGenerator.FindByGenerator(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        public IActionResult EditClient(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int id_int = Int32.Parse(id);
            // bug
            Customer obj = manageSuperAdminCustomer.FindByID(id_int);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Customer/Edit",obj);

        }

        [HttpPost]
        public IActionResult Edit(Generator obj)
        {

            if (ModelState.IsValid)
            {
                //manageSuperAdminUserRoles.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        
    }
}
