using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace ProjectManagementSystem.Hubs
{
    public class CommentHub : Hub
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public CommentHub(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task SendComment(int todoId, string commentContent)
        {

            // add the coomen in db:
            //var userName = Context.User?.Identity?.Name ?? "Anonymous";
            var u = Context.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(u);
            Comment comment = new Comment()
            {
                CommentContent = commentContent,
                CreatedAt = DateTime.Now,
                EmployeeId = user.Id,
                TodoId = todoId
            };
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();


            // call signalR function to display it on the page:
            await Clients.Group(todoId.ToString()).SendAsync("ReceiveComment", user.FirstName, commentContent, todoId, comment.CreatedAt.ToString("dd/MM/yyyy"));
        }

        public async Task JoinTodoGroup(int todoId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, todoId.ToString());
        }


        public async Task SendComment2()
        {
            int temp = 0;
        }
    }
}
