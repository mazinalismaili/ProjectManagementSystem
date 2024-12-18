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
    public class StatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Status
        public IActionResult Index()
        {
            return View( _unitOfWork.StatusRepository.GetAll());
        }

        // GET: Status/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            var status =  _unitOfWork.StatusRepository.Get(m => m.Id == id);
            if (status == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StatusRepository.Add(status);
                _unitOfWork.StatusRepository.Save();
                TempData["success"] = "Item has been created successfully";
                return RedirectToAction("Index");
            }
            return View(status);
        }

        // GET: Status/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            var status = _unitOfWork.StatusRepository.Get(s =>s.Id == id);
            if (status == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Status status)
        {
            if (id != status.Id)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.StatusRepository.Update(status);
                _unitOfWork.StatusRepository.Save();
                TempData["success"] = "Item has been edited successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be edited";
            return View(status);
        }

        // GET: Status/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            var status = _unitOfWork.StatusRepository.Get(m => m.Id == id);
            if (status == null)
            {
                TempData["error"] = "Item could not be found";
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedAsync(int id)
        {
            var status =  _unitOfWork.StatusRepository.Get(s => s.Id == id);
            var isUsed = _unitOfWork.TodoRepository.Get(s => s.StatusId == id);
            if(isUsed != null)
            {

                TempData["error"] = "Item could not be deleted. As it has some Task assigned to it";
                return View("Delete", status);
            }
            if (status != null)
            {
                _unitOfWork.StatusRepository.Remove(status);
                _unitOfWork.StatusRepository.Save();
                TempData["success"] = "Item has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be deleted";
            return View(status);
        }

        private bool StatusExists(int id)
        {
            if(_unitOfWork.StatusRepository.Get(e => e.Id == id) != null)
            {
                return true;
            }
            return true;
        }
    }
}
