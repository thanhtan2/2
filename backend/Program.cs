using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    // Thử kết nối SQL Server
    string connString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                        ?? "Server=db;Database=MyDb;User Id=sa;Password=YourStrong@Password;TrustServerCertificate=True;";
    
    string status = "Checking DB...";
    try {
        using (var conn = new SqlConnection(connString)) {
            conn.Open();
            status = "Connected to SQL Server successfully!";
        }
    } catch (Exception ex) {
        status = $"DB Error: {ex.Message}";
    }

    return $"Backend: Hello from ASP.NET Core! \nDB Status: {status}";
});

app.Run("http://0.0.0.0:8080");