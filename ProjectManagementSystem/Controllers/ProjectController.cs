using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Security.Claims;

namespace ProjectManagementSystem.Controllers
{

    
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;
        public ProjectController(IUnitOfWork uow, ApplicationDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        // GET: ProjectController
        public ActionResult Index()
        {
            List<Project> projects = _uow.ProjectRepository.GetAll().ToList();
            return View(projects);
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(int id)
        {
            var project = _uow.ProjectRepository.Get(i => i.Id == id);

            return View(project);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            ViewData["Employee"] = _context.Employee.Select(s => new SelectListItem
            {
                Text = s.UserName,
                Value = s.Id.ToString()
            });
            ViewData["Priority"] = _uow.PriorityRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            });

            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

            if (ModelState.IsValid)
            {
                Project project = new Project();

                project.Name = collection["Name"].ToString();
                project.CreatedDate = DateTime.Now;
                project.StartDate = DateTime.Parse(collection[key: "StartDate"]);
                project.EndDate = DateTime.Parse(collection["EndDate"]);
                project.Description = collection["Description"].ToString();
                project.EmployeeId = collection["EmployeeId"].ToString();
                project.PriorityId = int.Parse( collection["PriorityId"]);

                _uow.ProjectRepository.Add(project);
                _uow.ProjectRepository.Save();
                TempData["success"] = "Item has been created successfully";
                return RedirectToAction("Index");

            }

            TempData["error"] = "Item could not be created";
            return RedirectToAction("Index");
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Employee"] = _context.Employee.Select(s => new SelectListItem
            {
                Text = s.UserName,
                Value = s.Id.ToString()
            });
            ViewData["Priority"] = _uow.PriorityRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            });
            Project projectObj = _uow.ProjectRepository.Get(i => i.Id == id);
            return View(projectObj);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project obj)
        {
            try
            {
                _uow.ProjectRepository.Update(obj);
                _uow.ProjectRepository.Save();
                TempData["success"] = "Item has been edited successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Item could not be edited";
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            var project = _uow.ProjectRepository.Get(i => i.Id == id);
            if (project != null)
            { 
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var project = _uow.ProjectRepository.Get(i => i.Id == id);
            if(project != null)
            {
                _uow.ProjectRepository.Remove(project);
                _uow.ProjectRepository.Save();
                TempData["success"] = "Item has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be deleted";
            return View();
        }
    }
}
