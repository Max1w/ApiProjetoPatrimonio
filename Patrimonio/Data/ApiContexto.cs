using Microsoft.EntityFrameworkCore;
using Patrimonio.Modelos;

namespace Patrimonio.Data
{
	public class ApiContexto : DbContext
	{
		public DbSet<Item> Itens { get; set; }
		public DbSet<Aquisicao> Aquisicoes { get; set; }
		public DbSet<Depreciacao> Depreciacoes { get; set; }
		public DbSet<Localizacao> Localizacoes { get; set; }
		public DbSet<Veiculo> Veiculos { get; set; }
		public DbSet<HistoricoSaida> HistoricoSaidas { get; set; }

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
