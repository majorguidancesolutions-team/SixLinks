using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixLinksWeb.Data;
using DataLibrary;
using MyDataManagerDataOperations;
using SixLinksDataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

var connectionString2 = builder.Configuration.GetConnectionString("MyDataManagerData");
builder.Services.AddDbContext<DataDbContext>(options =>
	options.UseSqlServer(connectionString2), ServiceLifetime.Transient);

var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionString).Options;
using (var context = new ApplicationDbContext(contextOptions))
{
	context.Database.Migrate();
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDataOperations, DataOperations>();
builder.Services.AddScoped<ISixLinksData, SixLinksData>();
builder.Services.AddScoped<IUserRolesService, UserRolesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
