using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Dto;
using To_Do_List_Backend.Services;

namespace To_Do_List_Backend.Controllers
{
    [ApiController]
    [Route( "rest/[controller]" )]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController( ITodoService todoService )
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Route( "get-all" )]
        public ActionResult<List<Todo>> GetAll()
        {
            List<Todo> todos = _todoService.GetTodos();
            if ( todos.Count == 0 )
            {
                return NotFound();
            }
            return Ok( _todoService.GetTodos() );
        }

        [HttpGet]
        [Route( "{todoId}" )]
        public ActionResult<Todo> GetTodo( int todoId )
        {
            Todo? todo = _todoService.GetTodo( todoId );
            if ( todo == null )
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPost]
        [Route( "create" )]
        public IActionResult CreateTodo( [FromBody] TodoDto todoDto )
        {
            _todoService.CreateTodo( todoDto );
            return Ok("Todo was created");
        }

        [HttpPut]
        [Route( "{todoId}/complete" )]
        public IActionResult CompleteTodo( int todoId )
        {
            int rowAffected = _todoService.CompleteTodo( todoId );
            if ( rowAffected == 0 )
            {
                return NotFound();
            }
            return Ok("Todo was updated");
        }

        [HttpDelete]
        [Route( "{todoId}/delete" )]
        public IActionResult DeleteTodo( int todoId )
        {
            _todoService.DeleteTodo( todoId );
            return Ok("Todo was deleted");
        }
    }
}
