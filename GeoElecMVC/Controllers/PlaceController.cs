using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using GeoElecMVC.Models;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.SuperAdminPlace;

namespace GeoElecMVC.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ManageSuperAdminPlace manageSuperAdminPlace;

        public PlaceController(IConfiguration configuration)
        {
            manageSuperAdminPlace = new ManageSuperAdminPlace(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageSuperAdminPlace.FindAll());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Place/Create
        [HttpPost]
        public IActionResult Create(Place place_obj)
        {
            if (ModelState.IsValid)
            {
                manageSuperAdminPlace.Add(place_obj);
                return RedirectToAction("Index");
            }
            return View(place_obj);

        }

        // GET: /Place/Edit/1
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Place obj = manageSuperAdminPlace.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Place/Edit   
        [HttpPost]
        public IActionResult Edit(Place obj)
        {

            if (ModelState.IsValid)
            {
                manageSuperAdminPlace.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Place/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            manageSuperAdminPlace.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}
