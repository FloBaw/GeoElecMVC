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

namespace GeoElecMVC.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly ManageGeoGenerator manageGeoGenerator;


        public GeneratorController(IConfiguration configuration)
        {
            manageGeoGenerator = new ManageGeoGenerator(configuration);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            return View(manageGeoGenerator.FindAllGen());
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
            return View(manageGeoGenerator.FindAllClient());
            //return View("../Customer/Edit",obj);

        }

        public IActionResult EditClientForm(int? clientid, string generatorid)
        {
            Customer obj = manageGeoGenerator.FindCustByID(clientid.Value);
            if (ModelState.IsValid)
            {
                manageGeoGenerator.UpdateGenClient(generatorid, clientid.Value);
                return View("Edit", manageGeoGenerator.FindByGenerator(generatorid));
                //manageSuperAdminUserRoles.Update(obj);
                //return RedirectToAction("Index");
                //return View("../Customer/Edit");
            }
            //return View("../Customer/Edit", obj);
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
            return View(manageGeoGenerator.FindAllLessee());

        }

        public IActionResult EditLesseeForm(int? lesseeid, string generatorid)
        {
            Customer obj = manageGeoGenerator.FindCustByID(lesseeid.Value);
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
            return View(manageGeoGenerator.FindAllPlace());

        }

        public IActionResult EditPlaceForm(int? placeid, string generatorid)
        {
            Customer obj = manageGeoGenerator.FindCustByID(placeid.Value);
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
