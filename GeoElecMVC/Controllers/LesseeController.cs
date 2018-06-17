using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using GeoElecMVC.Models;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.SuperAdminLessee;

namespace GeoElecMVC.Controllers
{
    public class LesseeController : Controller
    {
        private readonly ManageSuperAdminLessee manageSuperAdminLessee;

        public LesseeController(IConfiguration configuration)
        {
            manageSuperAdminLessee = new ManageSuperAdminLessee(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageSuperAdminLessee.FindAll());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessee/Create
        [HttpPost]
        public IActionResult Create(Lessee lesse_obj)
        {
            if (ModelState.IsValid)
            {
                manageSuperAdminLessee.Add(lesse_obj);
                return RedirectToAction("Index");
            }
            return View(lesse_obj);

        }

        // GET: /Lessee/Edit/1
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Lessee obj = manageSuperAdminLessee.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Lessee/Edit   
        [HttpPost]
        public IActionResult Edit(Lessee obj)
        {

            if (ModelState.IsValid)
            {
                manageSuperAdminLessee.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Lessee/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            manageSuperAdminLessee.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}
