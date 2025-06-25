using TasksAPI.Models;

namespace TasksAPI.Services
{
    public interface ITaskCollectionService : ICollectionService<TaskModel>
    {
        public Task<List<TaskModel>> GetTasksByStatus(string status);
    }
}
