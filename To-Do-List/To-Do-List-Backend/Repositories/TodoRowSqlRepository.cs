using System.Data;
using System.Data.SqlClient;
using To_Do_List_Backend.Domain;

namespace To_Do_List_Backend.Repositories
{
    public class TodoRowSqlRepository : ITodoRepository
    {
        private readonly string _connectionString;

        public TodoRowSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public int Create( Todo todo )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( var command = connection.CreateCommand() )
                {
                    command.CommandText = @"INSERT INTO [dbo].[todo]([title], [is_done]) VALUES (@title, @is_done)";
                    command.Parameters.Add( "@title", SqlDbType.NVarChar ).Value = todo.Title;
                    command.Parameters.Add( "@lastName", SqlDbType.Bit ).Value = todo.IsDone;

                    return command.ExecuteNonQuery();
                }
            }
        }

        public void Delete( Todo todo )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( var command = connection.CreateCommand() )
                {
                    command.CommandText = @"DELETE FROM [dbo].[todo] WHERE [id_todo] = @id_todo";
                    command.Parameters.Add( "@id_todo", SqlDbType.Int ).Value = todo.Id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public Todo? Get( int id )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = @"SELECT [id_todo], [titile], [is_done] FROM [dbo].[todo] WHERE [id_todo] = @id_todo";
                    command.Parameters.Add( "@id_todo", SqlDbType.Int ).Value = id;

                    using ( var reader = command.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            return new Todo
                            {
                                Id = Convert.ToInt32( reader[ nameof( Todo.Id ) ] ),
                                Title = Convert.ToString( reader[ nameof( Todo.Title ) ] ),
                                IsDone = Convert.ToBoolean( reader[ nameof( Todo.IsDone ) ] )
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Todo> GetTodos()
        {
            List<Todo> todos = new List<Todo>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( var command = connection.CreateCommand() )
                {
                    command.CommandText = @"SELECT [id_todo], [title], [is_done] FROM [dbo].[todo]";

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            todos.Add( new Todo
                            {
                                Id = Convert.ToInt32( reader[ nameof( Todo.Id ) ] ),
                                Title = Convert.ToString( reader[ nameof( Todo.Title ) ] ),
                                IsDone = Convert.ToBoolean( reader[ nameof( Todo.IsDone ) ] )
                            } );
                        }
                    }
                }
            }

            return todos;
        }

        public int Update( Todo todo )
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [dbo].[todo] SET [titile] = @title, [is_done] = @is_done WHERE [id_todo] = @id_todo";
                    command.Parameters.Add("@id_todo", SqlDbType.Int).Value = todo.Id;
                    command.Parameters.Add("@title", SqlDbType.NVarChar).Value = todo.Title;
                    command.Parameters.Add("@is_done", SqlDbType.Bit).Value = todo.IsDone;

                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
