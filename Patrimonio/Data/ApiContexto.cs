using Microsoft.EntityFrameworkCore;
using Patrimonio.Modelos;

namespace Patrimonio.Data
{
	public class ApiContexto : DbContext
	{
		public DbSet<PatrimonioItens> Itens { get; set; }

		private IConfiguration _configuration;

		public ApiContexto(IConfiguration configuration, DbContextOptions options) : base(options)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var TypeDatabase = _configuration["TypeDatabase"];
			var connectionString = _configuration.GetConnectionString(TypeDatabase);

			if (TypeDatabase == "SqlServer")
			{
				optionsBuilder.UseSqlServer(connectionString);
			}
		}
	}
}
