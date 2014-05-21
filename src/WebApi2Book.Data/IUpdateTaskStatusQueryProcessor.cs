using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data
{
    public interface IUpdateTaskStatusQueryProcessor
    {
        void UpdateTaskStatus(Task taskToUpdate, string statusName);
    }
}