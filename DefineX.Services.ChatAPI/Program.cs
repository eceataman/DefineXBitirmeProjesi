using DefineX.Services.ChatAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 SignalR servisini burada ekle (Doğru yer burası!)
builder.Services.AddSignalR();

// ✅ CORS konfigürasyonu - localhost için esnek yapı
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowNuxt", policy =>
	{
		policy
			.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost") // localhost olan her porta izin ver
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials(); // SignalR için mutlaka
	});
});

var app = builder.Build();

// CORS politikasını uygula (SignalR öncesinde)
app.UseCors("AllowNuxt");

// Swagger ve diğer middleware'ler
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 🔥 ChatHub'u burada map et (Doğru yer burası!)
app.MapHub<ChatHub>("/chathub");

app.Run();
