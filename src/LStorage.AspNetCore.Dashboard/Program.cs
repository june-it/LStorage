using LStorage;
using LStorage.EntityFrameworkCore;
using LStorage.Locations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLStorage(x =>
{
    x.AddQuerier<EFQuerier<Area>, Area>();
    x.AddQuerier<EFQuerier<Shelf>, Shelf>();
    x.AddQuerier<EFQuerier<Location>, Location>();
    x.AddQuerier<EFQuerier<Pallet>, Pallet>();
    x.AddQuerier<EFQuerier<Material>, Material>();
    x.AddQuerier<EFQuerier<Inventory>, Inventory>();
    x.AddLocationAllocator<PalletShuttleLocationAllocator>();
});
builder.Services.AddDbContext<LStorageDbContext>(configure =>
{
    configure.UseMySql(builder.Configuration.GetConnectionString("Default"), new MySqlServerVersion("8.0"));
});

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
