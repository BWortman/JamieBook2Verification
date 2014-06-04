using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;
using WebApi2Book.Web.Common.Validation;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.JuniorWorker)]
    public class TasksController : ApiController
    {
        private readonly ITasksControllerDependencyBlock _tasksControllerDependencyBlock;

        public TasksController(ITasksControllerDependencyBlock tasksControllerDependencyBlock)
        {
            _tasksControllerDependencyBlock = tasksControllerDependencyBlock;
        }

        [Route("", Name = "GetTasksRoute")]
        public PagedDataInquiryResponse<Task> GetTasks(HttpRequestMessage requestMessage)
        {
            var request = _tasksControllerDependencyBlock.PagedDataRequestFactory.Create(requestMessage.RequestUri);

            var tasks = _tasksControllerDependencyBlock.AllTasksInquiryProcessor.GetTasks(request);
            return tasks;
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        public IHttpActionResult AddTask(HttpRequestMessage requestMessage, NewTask newTask)
        {
            var task = _tasksControllerDependencyBlock.AddTaskMaintenanceProcessor.AddTask(newTask);
            var result = new TaskCreatedActionResult(requestMessage, task);
            return result;
        }

        [Route("{id:long}", Name = "GetTaskRoute")]
        public Task GetTask(long id)
        {
            var task = _tasksControllerDependencyBlock.TaskByIdInquiryProcessor.GetTask(id);
            return task;
        }

        [Route("{id:long}", Name = "UpdateTaskRoute")]
        [HttpPut]
        [HttpPatch]
        [ValidateTaskUpdateRequest]
        [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
        public Task UpdateTask(long id, [FromBody] object updatedTask)
        {
            var task = _tasksControllerDependencyBlock.UpdateTaskMaintenanceProcessor.UpdateTask(id, updatedTask);
            return task;
        }
    }
}
