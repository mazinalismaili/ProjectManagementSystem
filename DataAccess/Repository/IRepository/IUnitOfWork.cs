using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork{
        //IMainRepository iRepository { get; }
        //void Save();

        IMainRepository<Project> ProjectRepository { get; }
        IMainRepository<Todo> TodoRepository { get; }
        IMainRepository<Status> StatusRepository { get; }
        IMainRepository<Priority> PriorityRepository { get; }
        IMainRepository<Comment> CommentRepository { get; }


    }
}
