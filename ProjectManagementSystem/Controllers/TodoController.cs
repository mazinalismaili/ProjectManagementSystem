using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace ProjectManagementSystem.Controllers
{
    public class TodoController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        public TodoController(ApplicationDbContext context, IUnitOfWork uow, UserManager<Employee> userManager)
        {
            _context = context;
            _uow = uow;
            _userManager = userManager;
        }

        // GET: TodoController
        public ActionResult Index()
        {
            List<Todo> todos = _uow.TodoRepository.GetAll().ToList();
            return View(todos);
        }

        // GET: TodoController/Details/5
        public ActionResult Details(int id)
        {
            var todo = _uow.TodoRepository.Get(i => i.Id == id);
            return View(todo);
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            ViewData["Employee"] = _context.Employee.Select(s => new SelectListItem
            {
                Text = s.UserName,
                Value = s.Id.ToString()
            });
            ViewData["Status"] = _uow.StatusRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            });
            ViewData["Project"] = _uow.ProjectRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            });
            return View();
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Todo todo = new Todo();

                todo.Name = collection["Name"].ToString();
                todo.CreateDate = DateTime.Now;
                todo.StartDate = DateTime.Parse(collection["StartDate"].ToString());
                todo.EndDate = DateTime.Parse(collection["EndDate"].ToString());
                todo.Description = collection["Description"].ToString();
                todo.StatusId = int.Parse(collection["StatusId"].ToString());
                todo.EmployeeId = collection["EmployeeId"].ToString();
                todo.ProjectId = int.Parse(collection["ProjectId"].ToString());

                _uow.TodoRepository.Add(todo);
                _uow.TodoRepository.Save();
                TempData["success"] = "Item has been created successfully";
                return RedirectToAction("Index");

            }
            TempData["error"] = "Item could not be created";
            return RedirectToAction("Index");
        }

        // GET: TodoController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Employee"] = _context.Employee.Select(s => new SelectListItem
            {
                Text = s.UserName,
                Value = s.Id.ToString()
            });
            ViewData["Status"] = _uow.StatusRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            });
            ViewData["Project"] = _uow.ProjectRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            });
            Todo todoObj = _uow.TodoRepository.Get(i => i.Id == id);
            return View(todoObj);
        }

        // POST: TodoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Todo newObj)
        {
            try
            {
                _uow.TodoRepository.Update(newObj);
                _uow.TodoRepository.Save();
                TempData["success"] = "Item has been edited successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Item could not be edited";
                return View();
            }
        }

        // GET: TodoController/Delete/5
        public ActionResult Delete(int id)
        {
            var todo = _uow.TodoRepository.Get( i=>i.Id == id);
            if (todo != null)
            {
                return View(todo);
            }
            return RedirectToAction("Index");
        }

        // POST: TodoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            var todo = _uow.TodoRepository.Get(i => i.Id == id);
            if (todo != null)
            {
                // delted all comments first:

                // delete the task:
                _uow.TodoRepository.Remove(todo);
                _uow.TodoRepository.Save();

                TempData["success"] = "Item has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Item could not be deleted";
            return View();
        }

        //[HttpPost]
        public ActionResult DeleteComment(int id, int todoId)
        {
            if(id == null || id <= 1)
            {
                TempData["error"] = "Comment can not be found.";
                return NotFound();
            }

            var comment = _context.Comment.Find(id);
            if(comment == null)
            {
                TempData["error"] = "Comment can not be found.";
                return NotFound();
            }

            _context.Comment.Remove(comment);
            _context.SaveChanges();

            TempData["success"] = "Comment has been deleted successfully";
            return RedirectToAction("Details", "Todo",new {id = todoId});
        }


        public ActionResult ReportComment(int id, int todoId)
        {
            TempData["success"] = "Comment has been reported successfully";
            return RedirectToAction("Details", "Todo", new { id = todoId });
        }
        public async Task<IActionResult> AddComment(string CommentSection, int Id)
        {
            var user = await _userManager.GetUserAsync(User);
            Comment comment = new Comment()
            {
                CommentContent = CommentSection,
                CreatedAt = DateTime.Now,
                EmployeeId = user.Id,
                TodoId = Id
            };
            //_context.Set<Comment>().Add(comment);
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = Id });
        }
    }
}
