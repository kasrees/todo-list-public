using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Dto;
using To_Do_List_Backend.Repositories;

namespace To_Do_List_Backend.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService( ITodoRepository todoRepository )
        {
            _todoRepository = todoRepository;
        }

        public int CompleteTodo( int todoId )
        {
            Todo? todo = _todoRepository.Get( todoId );
            if ( todo == null )
            {
                return 0;
            }
            todo.IsDone = true;
            return _todoRepository.Update( todo );
        }

        public void CreateTodo( TodoDto todo )
        {
            _todoRepository.Create( todo.ToTodo() );
        }

        public void DeleteTodo( int todoId )
        {
            _todoRepository.Delete( new Todo { Id = todoId } );
        }

        public Todo GetTodo( int todoId )
        {
            Todo? todo = _todoRepository.Get( todoId );
            if ( todo == null )
            {
                return null;
            }

            return todo;
        }

        public List<Todo> GetTodos()
        {
            return _todoRepository.GetTodos();
        }
    }
}
