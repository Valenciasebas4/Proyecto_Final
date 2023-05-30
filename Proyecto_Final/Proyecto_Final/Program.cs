using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*se define una conexion default para la base de datos*/
builder.Services.AddDbContext<DataBaseContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Builder para llamar la clase SeederDb.cs|
builder.Services.AddTransient<SeederDb>();


var app = builder.Build();


SeederData();
void SeederData()
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        SeederDb? service = scope.ServiceProvider.GetService<SeederDb>();
        service.SeedAsync().Wait();
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
