using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group23.Data; 
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddControllersWithViews(); // Use this for MVC
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Make sure you have an Error action in your HomeController for handling exceptions.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


void BackupDatabase()
{
    string serverName = "localhost"; // Adjust if your SQL Server instance has a different name or is on a remote server
    string databaseName = "TravelDB"; // Your database name
    string backupDirectory = @"C:\Semester 4\Comp2139\Assignment1\Back-Up";
    string backupFileName = "TravelDB_Backup_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
    string backupPath = System.IO.Path.Combine(backupDirectory, backupFileName);

    try
    {
        // Create server connection
        ServerConnection serverConnection = new ServerConnection(serverName);
        Server server = new Server(serverConnection);

        // Check if the database exists
        if (server.Databases.Contains(databaseName))
        {
            // Get reference to the database
            Database database = server.Databases[databaseName];

            // Create a new backup operation
            Backup backup = new Backup
            {
                Action = BackupActionType.Database,
                Database = databaseName
            };

            // Specify the backup device (file)
            backup.Devices.AddDevice(backupPath, DeviceType.File);

            // Perform the backup
            backup.SqlBackup(server);

            Console.WriteLine("Backup completed successfully to " + backupPath);
        }
        else
        {
            Console.WriteLine($"Database '{databaseName}' does not exist on server '{serverName}'.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}


app.Run();
