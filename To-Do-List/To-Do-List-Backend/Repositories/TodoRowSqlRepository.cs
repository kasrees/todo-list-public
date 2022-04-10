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
                    command.CommandText = @"INSERT INTO [dbo].[Todo] ([Title], [IsDone]) VALUES (@title, @isDone); SELECT SCOPE_IDENTITY();";
                    command.Parameters.Add( "@title", SqlDbType.NVarChar ).Value = todo.Title;
                    command.Parameters.Add( "@isDone", SqlDbType.Bit ).Value = todo.IsDone;

                    return Convert.ToInt32( command.ExecuteScalar() );
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
                    command.CommandText = @"DELETE FROM [dbo].[Todo] WHERE [Id] = @id";
                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = todo.Id;

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
                    command.CommandText = @"SELECT [Id], [Title], [IsDone] FROM [dbo].[Todo] WHERE [Id] = @id";
                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = id;

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
                    command.CommandText = @"SELECT [Id], [Title], [IsDone] FROM [dbo].[Todo]";

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
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( var command = connection.CreateCommand() )
                {
                    command.CommandText = @"UPDATE [dbo].[Todo] SET [Title] = @title, [IsDone] = @isDone WHERE [Id] = @id";
                    command.Parameters.Add( "@id", SqlDbType.Int ).Value = todo.Id;
                    command.Parameters.Add( "@title", SqlDbType.NVarChar ).Value = todo.Title;
                    command.Parameters.Add( "@isDone", SqlDbType.Bit ).Value = todo.IsDone;

                    command.ExecuteNonQuery();
                    return todo.Id;
                }
            }
        }
    }
}
