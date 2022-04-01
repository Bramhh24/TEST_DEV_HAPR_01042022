using Dapper;
using FrontWeb.Models;
using System.Data.SqlClient;

namespace FrontWeb.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task Actualizar(Usuario usuario);
        Task Borrar(int id);
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<Usuario> BuscarUsuarioPorId(int id);
        Task<int> CrearUsuario(Usuario usuario);
    }
    public class RepositorioUsuarios: IRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public  async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"
                INSERT INTO Usuarios (Nombre, ApellidoPaterno, ApellidoMaterno, Email, EmailNormalizado, PasswordHash)
                VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Email, @EmailNormalizado, @PasswordHash);
                SELECT SCOPE_IDENTITY();
                    ", usuario);

            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuarios WHERE EmailNormalizado = @emailNormalizado", new { emailNormalizado });
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuarios WHERE Id = @Id", new { id });
        }

        public async Task Actualizar(Usuario usuario)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Usuarios 
                                            SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, 
                                            ApellidoMaterno = @ApellidoMaterno WHERE Id = @Id;", usuario);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Usuarios WHERE Id = @Id;", new { id });
        }
    }
}
