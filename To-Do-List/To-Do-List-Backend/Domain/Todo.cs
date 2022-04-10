using To_Do_List_Backend.Dto;

namespace To_Do_List_Backend.Domain
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public TodoDto ToTodoDto ()
        {
            return new TodoDto
            {
                Id = Id,
                Title = Title,
                IsDone = IsDone
            };
        }
    }
}
