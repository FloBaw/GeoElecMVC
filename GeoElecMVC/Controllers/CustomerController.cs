using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using GeoElecMVC.Models;
using GeoElecMVC.Repository;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.AdminCustomer;

namespace GeoElecMVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ManageAdminCustomer manageAdminCustomer;

        public CustomerController(IConfiguration configuration)
        {
            manageAdminCustomer = new ManageAdminCustomer(configuration);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(manageAdminCustomer.FindAll());
        }

        [Authorize(Roles = "Admin")]
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
                manageAdminCustomer.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /Customer/Edit/1
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer obj = manageAdminCustomer.FindByID(id.Value);
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
                manageAdminCustomer.Update(obj);
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
            manageAdminCustomer.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}
