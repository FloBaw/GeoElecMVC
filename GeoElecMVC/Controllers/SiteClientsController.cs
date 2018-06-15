using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.SuperAdminSiteClients;

namespace GeoElecMVC.Controllers
{
    public class SiteClientsController : Controller
    {
        private readonly ManageSuperAdminSiteClients manageSuperAdminSiteClients;

        public SiteClientsController(IConfiguration configuration)
        {
            manageSuperAdminSiteClients = new ManageSuperAdminSiteClients(configuration);

        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageSuperAdminSiteClients.FindAll());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRole/Create
        [HttpPost]
        public IActionResult Create(SiteClients cust)
        {
            if (ModelState.IsValid)
            {
                manageSuperAdminSiteClients.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /UserRole/Edit/1
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SiteClients obj = manageSuperAdminSiteClients.FindByUserName(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // GET: /UserRole/EditAwaiting/1
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult EditAwaiting(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SiteClients obj = manageSuperAdminSiteClients.FindAwaitingUserByUserName(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Create", obj);
        }


        // POST: /UserRole/Edit   
        [HttpPost]
        public IActionResult Edit(SiteClients obj)
        {

            if (ModelState.IsValid)
            {
                manageSuperAdminSiteClients.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/UserRole/Delete/1
        public IActionResult Delete(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            manageSuperAdminSiteClients.Remove(id);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Awaiting()
        {
            return View(manageSuperAdminSiteClients.FindAwaitingUsers());
        }
    }
}
