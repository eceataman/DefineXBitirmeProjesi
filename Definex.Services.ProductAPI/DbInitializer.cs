using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System;
using Definex.Services.ProductAPI.DbContexts;
using Definex.Services.ProductAPI.Models;

public static class DbInitializer
{
	public static void SeedDatabase(IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		context.Database.Migrate(); // otomatik migrate olsun

		if (!context.Products.Any())
		{
			var jsonData = File.ReadAllText("Data/products.json");
			var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
			context.Products.AddRange(products);
			context.SaveChanges();
		}
	}
}
