using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using GeoElecMVC.Models;
using GeoElecMVC.Repository;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.SuperAdminCustomer;

namespace GeoElecMVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ManageSuperAdminCustomer manageSuperAdminCustomer;

        public CustomerController(IConfiguration configuration)
        {
            manageSuperAdminCustomer = new ManageSuperAdminCustomer(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageSuperAdminCustomer.FindAll());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(Customer cust)
        {
            if (ModelState.IsValid)
            {
                manageSuperAdminCustomer.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /Customer/Edit/1
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer obj = manageSuperAdminCustomer.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Customer/Edit   
        [HttpPost]
        public IActionResult Edit(Customer obj)
        {

            if (ModelState.IsValid)
            {
                manageSuperAdminCustomer.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Customer/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            manageSuperAdminCustomer.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}
