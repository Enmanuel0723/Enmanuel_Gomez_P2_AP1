using Enmanuel_Gomez_P2_AP1.Components;
using Enmanuel_Gomez_P2_AP1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// inyectando conection string
var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContextFactory<Context>(options => options.UseSqlServer(ConStr));


// inyectando servicios
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<EncuestasService>();
builder.Services.AddScoped<CiudadesService>();



// los servicios y el conection string van arribad esta linea de codigo
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
