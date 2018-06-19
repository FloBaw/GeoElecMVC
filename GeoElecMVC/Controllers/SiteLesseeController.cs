using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.SuperAdminSiteLessee;
using GeoElecMVC.SuperAdminLessee;

namespace GeoElecMVC.Controllers
{
    public class SiteLesseeController : Controller
    {
        private readonly ManageSuperAdminSiteLessee manageSuperAdminSiteLessee;
        private readonly ManageSuperAdminLessee manageSuperAdminLessee;

        public SiteLesseeController(IConfiguration configuration)
        {
            manageSuperAdminSiteLessee = new ManageSuperAdminSiteLessee(configuration);
            manageSuperAdminLessee = new ManageSuperAdminLessee(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageSuperAdminSiteLessee.FindAll());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Awaiting()
        {
            return View(manageSuperAdminSiteLessee.FindAwaitingUsers());
        }

        // GET: /SiteLessee/Edit/1
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SiteLessee obj = manageSuperAdminSiteLessee.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /SiteLessee/Edit   
        [HttpPost]
        public IActionResult Edit(SiteLessee obj)
        {

            if (ModelState.IsValid)
            {
                manageSuperAdminSiteLessee.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = id;
            return View("Create", manageSuperAdminLessee.FindAll());
        }

        // POST: SiteLessee/Create
        [HttpPost]
        public IActionResult Create(int? lesseeid, string username)
        {
            manageSuperAdminSiteLessee.Add(username, lesseeid.Value);
            return RedirectToAction("Index");
        }

        // GET:/SiteLessee/Delete/1
        public IActionResult Delete(int id)
        {

            if (id.ToString() == null)
            {
                return NotFound();
            }
            manageSuperAdminSiteLessee.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult GoToIndexLessee()
        {
            return Redirect("../Lessee/Index");
        }

    }
}
