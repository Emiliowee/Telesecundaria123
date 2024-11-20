using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria;

var builder = WebApplication.CreateBuilder(args);

// Agrega los servicios al contenedor.
builder.Services.AddControllersWithViews();

// Lee la cadena de conexión desde el archivo appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura el contexto de la base de datos (Entity Framework Core).
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configura el logging (opcional).
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Agregar servicios de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Usar sesiones
app.UseSession();

// Configura el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
