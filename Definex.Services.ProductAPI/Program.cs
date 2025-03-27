using Definex.Services.ProductAPI.DbContexts;
using DefineX.Services.ProductAPI.Mapping;
using DefineX.Services.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.Authority = builder.Configuration["IdentityServer:Authority"];
		options.RequireHttpsMetadata = false; // 🔥 Sertifika zorunluluğunu kapat
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false,
			RoleClaimType = "role" // 🔥 'role' claim'ini tanı
		};
	});



builder.Services.AddAuthorization();

// 🌐 CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

// App oluşturuluyor
var app = builder.Build();

// 🔁 Seed işlemi
DbInitializer.SeedDatabase(app);

// Middleware sırası
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// ✅ Doğru sıra bu:
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
