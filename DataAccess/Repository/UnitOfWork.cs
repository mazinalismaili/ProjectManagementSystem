using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IMainRepository<Project> ProjectRepository { get; private set; }
        public IMainRepository<Todo> TodoRepository { get; private set; }
        public IMainRepository<Status> StatusRepository { get; private set; }
        public IMainRepository<Priority> PriorityRepository { get; private set; }
        public IMainRepository<Comment> CommentRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProjectRepository = new MainRepository<Project>(_context);
            TodoRepository = new MainRepository<Todo>(_context);
            StatusRepository = new MainRepository<Status>(_context);
            PriorityRepository = new MainRepository<Priority>(_context);
            CommentRepository = new MainRepository<Comment>(_context);
        }
    }
}
