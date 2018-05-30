using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using AuthPostMVC.Models;
using AuthPostMVC.AdminUserRoles;

namespace AuthPostMVC.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly ManageAdminUserRoles manageAdminUserRoles;

        public UserRolesController(IConfiguration configuration)
        {
            manageAdminUserRoles = new ManageAdminUserRoles(configuration);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(manageAdminUserRoles.FindAll());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(UserRoles cust)
        {
            if (ModelState.IsValid)
            {
                manageAdminUserRoles.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }
        
        // GET: /Customer/Edit/1
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserRoles obj = manageAdminUserRoles.FindByUserName(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

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

        // POST: /Customer/Edit   
        [HttpPost]
        public IActionResult Edit(UserRoles obj)
        {

            if (ModelState.IsValid)
            {
                manageAdminUserRoles.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Customer/Delete/1
        public IActionResult Delete(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            manageAdminUserRoles.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
