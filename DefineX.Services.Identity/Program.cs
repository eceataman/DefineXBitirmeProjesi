using DefineX.Services.Identity.Initializer;
using DefineX.Services.Identity.Models;
using DefineX.Services.Identity;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DefineX.Services.Identity.DbContexts;
using DefineX.Services.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ CORS ekleniyor
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy
			.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost") // Tüm localhost portlarına izin
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

var builderProvider = builder.Services.AddIdentityServer(options =>
{
	options.Events.RaiseErrorEvents = true;
	options.Events.RaiseInformationEvents = true;
	options.Events.RaiseFailureEvents = true;
	options.Events.RaiseSuccessEvents = true;
	options.EmitStaticAudienceClaim = true;
})
.AddInMemoryIdentityResources(SD.IdentityResources)
.AddInMemoryApiScopes(SD.ApiScopes)
.AddInMemoryClients(SD.Clients)
.AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builderProvider.AddDeveloperSigningCredential();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ CORS middleware aktif ediliyor (routing'den sonra, auth'den önce)
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

// Seed işlemi
using (var serviceScope = app.Services.CreateScope())
{
	var service = serviceScope.ServiceProvider.GetService<IDbInitializer>();
	service.Initialize();
}

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
