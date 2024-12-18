using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Models.Models;
using DataAccess.Repository.IRepository;

namespace ProjectManagementSystem.Controllers
{
    public class PriorityController : Controller
    {
        private readonly IUnitOfWork _uow;

        public PriorityController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Priority
        public IActionResult Index()
        {
            return View(_uow.PriorityRepository.GetAll());
        }

        // GET: Priority/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            var priority = _uow.PriorityRepository.Get(m => m.Id == id);
            if (priority == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            return View(priority);
        }

        // GET: Priority/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Priority/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Priority priority)
        {
            //ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                //priority.Id = Guid.NewGuid().ToString();
                _uow.PriorityRepository.Add(priority);
                _uow.PriorityRepository.Save();
                TempData["success"] = "Item has been created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be created";
            return View(priority);
        }

        // GET: Priority/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            var priority = _uow.PriorityRepository.Get(m => m.Id == id);
            if (priority == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }
            return View(priority);
        }

        // POST: Priority/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Priority priority)
        {
            if (id != priority.Id)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PriorityRepository.Update(priority);
                _uow.PriorityRepository.Save();
                TempData["success"] = "Item has been edited successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be edited";

            return View(priority);
        }

        // GET: Priority/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            var priority = _uow.PriorityRepository.Get(m => m.Id == id);
            if (priority == null)
            {
                TempData["error"] = "Item could not be found";

                return NotFound();
            }

            return View(priority);
        }

        // POST: Priority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmedAsync(int id)
        {
            var priority =  _uow.PriorityRepository.Get(m => m.Id == id);
            var assosiatedObjects = _uow.ProjectRepository.Get(m => m.PriorityId == id);
            if (assosiatedObjects != null)
            {

                TempData["error"] = "Item could not be deleted. As it has some Projects assosiated to it";
                return View("Delete", priority);
            }
            if (priority != null)
            {
                _uow.PriorityRepository.Remove(priority);
                _uow.PriorityRepository.Save();
                TempData["success"] = "Item has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be deleted";

            return RedirectToAction("Index");
        }

        private bool PriorityExists(int id)
        {
            if(_uow.PriorityRepository.Get(e => e.Id == id) != null)
            {  return true; }
            return false;
        }
    }
}
