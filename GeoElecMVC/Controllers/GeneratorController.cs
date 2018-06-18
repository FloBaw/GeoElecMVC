using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using GeoElecMVC.Models;
using GeoElecMVC.GeoGenerator;
using System.Security.Claims;

using GeoElecMVC.SuperAdminCustomer;
using GeoElecMVC.SuperAdminLessee;
using GeoElecMVC.SuperAdminPlace;

namespace GeoElecMVC.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly ManageGeoGenerator manageGeoGenerator;
        private readonly ManageSuperAdminCustomer manageSuperAdminCustomer;
        private readonly ManageSuperAdminLessee manageSuperAdminLessee;
        private readonly ManageSuperAdminPlace manageSuperAdminPlace;


        public GeneratorController(IConfiguration configuration)
        {
            manageGeoGenerator = new ManageGeoGenerator(configuration);
            manageSuperAdminCustomer = new ManageSuperAdminCustomer(configuration);
            manageSuperAdminLessee = new ManageSuperAdminLessee(configuration);
            manageSuperAdminPlace = new ManageSuperAdminPlace(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageGeoGenerator.FindAllGen());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generator/Create
        [HttpPost]
        public IActionResult Create(Generator gen_obj)
        {
            if (ModelState.IsValid)
            {
                manageGeoGenerator.Add(gen_obj);
                return RedirectToAction("Index");
            }
            return View(gen_obj);

        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Awaiting()
        {
            return View(manageGeoGenerator.FindAwaitingGen());
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
            ViewData["generatorId"] = obj.Generator_id;
            return View(obj);

        }

        public IActionResult EditAwaiting(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Generator obj = manageGeoGenerator.FindByAwaitingGenerator(id);
            ViewData["generatorId"] = obj.Generator_id;
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        public IActionResult EditClientGen(int? id, string generatorid)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["generatorId"] = generatorid;
            ViewData["clientId"] = id;
            return View(manageSuperAdminCustomer.FindAll());
            //return View("../Customer/Edit",obj);

        }

        public IActionResult EditClientForm(int? clientid, string generatorid)
        {
            Generator obj = manageGeoGenerator.FindByGenerator(generatorid);

            if (obj == null)
            {
                Generator objA = manageGeoGenerator.FindByAwaitingGenerator(generatorid);
                if (objA == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    manageGeoGenerator.UpdateGenClient(generatorid, clientid.Value);
                    return View("EditAwaiting", manageGeoGenerator.FindByAwaitingGenerator(generatorid));
                }
                
            }

            if (ModelState.IsValid)
            {
                manageGeoGenerator.UpdateGenClient(generatorid, clientid.Value);
                return View("Edit", manageGeoGenerator.FindByGenerator(generatorid));
            }
            return RedirectToAction("Index");
        }

        public IActionResult GoToIndexClient()
        {
            //return RedirectToAction("../Customer/Index");
            return Redirect("../Customer/Index");
        }


        public IActionResult EditLesseeGen(int? id, string generatorid)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["generatorId"] = generatorid;
            ViewData["lesseeId"] = id;
            return View(manageSuperAdminLessee.FindAll());

        }

        public IActionResult EditLesseeForm(int? lesseeid, string generatorid)
        {
            Generator obj = manageGeoGenerator.FindByGenerator(generatorid);

            if (obj == null)
            {
                Generator objA = manageGeoGenerator.FindByAwaitingGenerator(generatorid);
                if (objA == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    manageGeoGenerator.UpdateGenLessee(generatorid, lesseeid.Value);
                    return View("EditAwaiting", manageGeoGenerator.FindByAwaitingGenerator(generatorid));
                }

            }

            if (ModelState.IsValid)
            {
                manageGeoGenerator.UpdateGenLessee(generatorid, lesseeid.Value);
                return View("Edit", manageGeoGenerator.FindByGenerator(generatorid));
            }
            return RedirectToAction("Index");
        }


        public IActionResult GoToIndexLessee()
        {
            return Redirect("../Lessee/Index");
        }

        public IActionResult EditPlaceGen(int? id, string generatorid)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["generatorId"] = generatorid;
            ViewData["placeId"] = id;
            return View(manageSuperAdminPlace.FindAll());
            //return View(manageGeoGenerator.FindAllPlace());

        }

        public IActionResult EditPlaceForm(int? placeid, string generatorid)
        {
            Generator obj = manageGeoGenerator.FindByGenerator(generatorid);

            if (obj == null)
            {
                Generator objA = manageGeoGenerator.FindByAwaitingGenerator(generatorid);
                if (objA == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    manageGeoGenerator.UpdateGenPlace(generatorid, placeid.Value);
                    return View("EditAwaiting", manageGeoGenerator.FindByAwaitingGenerator(generatorid));
                }

            }

            if (ModelState.IsValid)
            {
                manageGeoGenerator.UpdateGenPlace(generatorid, placeid.Value);
                return View("Edit", manageGeoGenerator.FindByGenerator(generatorid));
            }
            return RedirectToAction("Index");
        }

        public IActionResult GoToIndexPlace()
        {
            return Redirect("../Place/Index");
        }

        [HttpPost]
        public IActionResult Edit(Generator obj)
        {

            if (ModelState.IsValid)
            {
                manageGeoGenerator.UpdateGen(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
