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

        public Todo? CompleteTodo( int todoId )
        {
            Todo? todo = _todoRepository.Get( todoId );

            if ( todo == null )
            {
                return null;
            }
            todo.IsDone = true;
            _todoRepository.Update( todo );

            return todo;
        }

        public Todo? CreateTodo( TodoDto todo )
        {
            int todoId = _todoRepository.Create( todo.ToTodo() );
            return GetTodo( todoId );
        }

        public void DeleteTodo( int todoId )
        {
            _todoRepository.Delete( new Todo { Id = todoId } );
        }

        public Todo? GetTodo( int todoId )
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
