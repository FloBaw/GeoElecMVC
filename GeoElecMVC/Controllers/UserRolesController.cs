using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.SuperAdminUserRoles;

namespace GeoElecMVC.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly ManageSuperAdminUserRoles manageSuperAdminUserRoles;

        public UserRolesController(IConfiguration configuration)
        {
            manageSuperAdminUserRoles = new ManageSuperAdminUserRoles(configuration);

        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageSuperAdminUserRoles.FindAll());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRole/Create
        [HttpPost]
        public IActionResult Create(UserRoles cust)
        {
            if (ModelState.IsValid)
            {
                manageSuperAdminUserRoles.Add(cust);
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
            UserRoles obj = manageSuperAdminUserRoles.FindByUserName(id);
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
            UserRoles obj = manageSuperAdminUserRoles.FindAwaitingUserByUserName(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Create", obj);
        }

        /*
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserRoles obj = manageAdminUserRoles.FindByID(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        */

        // POST: /UserRole/Edit   
        [HttpPost]
        public IActionResult Edit(UserRoles obj)
        {

            if (ModelState.IsValid)
            {
                manageSuperAdminUserRoles.Update(obj);
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
            manageSuperAdminUserRoles.Remove(id);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Awaiting()
        {
            return View(manageSuperAdminUserRoles.FindAwaitingUsers());
        }

        // GET:/UserRole/Delete/1
        public IActionResult DeleteAwaiting(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            manageSuperAdminUserRoles.RemoveAwaiting(id);
            return RedirectToAction("Awaiting");
        }
    }
}
