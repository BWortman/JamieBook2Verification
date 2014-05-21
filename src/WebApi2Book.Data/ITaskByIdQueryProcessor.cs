using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data
{
    public interface ITaskByIdQueryProcessor
    {
        Task GetTask(long taskId);
    }
}

