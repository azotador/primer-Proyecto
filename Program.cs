using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string connectionString = "Server=localhost\\SQLEXPRESS;Database=EmprendimientoDB;User Id=miUsuarioWeb;Password=Maximo13+4*;TrustServerCertificate=True;";

app.MapPost("/contacto", async (HttpRequest request) =>
{
    var form = await request.ReadFormAsync();
    var nombre = form["nombre"].ToString();
    var email = form["email"].ToString();
    var mensaje = form["mensaje"].ToString();

    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var query = "INSERT INTO Contactos (Nombre, Email, Mensaje) VALUES (@Nombre, @Email, @Mensaje)";
    using var cmd = new SqlCommand(query, connection);
    cmd.Parameters.AddWithValue("@Nombre", nombre);
    cmd.Parameters.AddWithValue("@Email", email);
    cmd.Parameters.AddWithValue("@Mensaje", mensaje);

    await cmd.ExecuteNonQueryAsync();

    return Results.Ok("Mensaje guardado correctamente ✅");
});

app.Run();
