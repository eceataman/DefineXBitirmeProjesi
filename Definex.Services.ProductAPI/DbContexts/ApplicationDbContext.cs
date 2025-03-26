using Definex.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Definex.Services.ProductAPI.DbContexts
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
	}
}
