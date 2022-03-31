using Dapper;
using PruebaToka.Models;
using System.Data.SqlClient;

namespace PruebaToka.Servicios
{
    public interface IRepositorioPersonasFisicas
    {
        Task Actualizar(PersonaFisica personaFisica);
        Task Borrar(int id);
        Task Crear(PersonaFisica personaFisica);
        Task<IEnumerable<PersonaFisica>> Obtener();
        Task<PersonaFisica> ObtenerPorId(int id);
    }
    public class RepositorioPersonasFisicas: IRepositorioPersonasFisicas
    {
        private readonly string connectionString;
        public RepositorioPersonasFisicas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(PersonaFisica personaFisica)
        {
                using var connection = new SqlConnection(connectionString);
                await connection.QuerySingleAsync("sp_AgregarPersonaFisica",
                    new
                    {
                        personaFisica.Nombre,
                        personaFisica.ApellidoPaterno,
                        personaFisica.ApellidoMaterno,
                        personaFisica.RFC,
                        personaFisica.FechaNacimiento,
                        personaFisica.UsuarioAgrega
                    }, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<PersonaFisica>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<PersonaFisica>("SELECT * FROM Tb_PersonasFisicas");
        }

        public async Task<PersonaFisica> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<PersonaFisica>(
                @"SELECT * FROM Tb_PersonasFisicas WHERE IdPersonaFisica = @Id", new {id});
        }

        public async Task Actualizar(PersonaFisica personaFisica)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("sp_ActualizarPersonaFisica",
                new
                {
                    personaFisica.IdPersonaFisica,
                    personaFisica.Nombre,
                    personaFisica.ApellidoPaterno,
                    personaFisica.ApellidoMaterno,
                    personaFisica.RFC,
                    personaFisica.FechaNacimiento,
                    personaFisica.UsuarioAgrega
                }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Borrar(int IdPersonaFisica)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("sp_EliminarPersonaFisica",
                new { IdPersonaFisica }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
