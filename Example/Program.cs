using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repisitories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
//.AddRazorRuntimeCompilation()'ý sayfa yenilediðimizde deðiþikliklerin görünmesi için ekledik
//Kullanmak için Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation paketini yükledikten sonra buraya bu þekilde tanýmlamamýz yeterlidir

builder.Services.AddDbContext<DosyaDbContext>();

builder.Services.AddScoped<IDosyaService, DosyaManager>();
builder.Services.AddScoped<IDosyaDal, DosyaRepository>();

var app = builder.Build();

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
